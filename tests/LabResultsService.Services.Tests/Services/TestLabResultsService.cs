using AutoFixture;
using LabResultsService.Core.Exceptions;
using LabResultsService.Repository.Data.Models;
using LabResultsService.Repository.Interfaces;
using LabResultsService.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using LabResultsServiceImpl = LabResultsService.Services.Services.LabResultsService;

namespace LabResultsService.Services.Tests.Services
{
    [TestFixture]
    public class TestLabResultsService
    {
        private Mock<ILabResultsRepository> _mockLabResultsRepository;
        private Mock<IPatientService> _mockPatientService;
        private Mock<ILogger<LabResultsServiceImpl>> _mockLogger;

        private ILabResultsService underTest;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _mockLabResultsRepository = new Mock<ILabResultsRepository>();
            _mockPatientService = new Mock<IPatientService>();
            _mockLogger = new Mock<ILogger<LabResultsServiceImpl>>();

            _fixture = new Fixture();
            underTest = new LabResultsServiceImpl(
                    _mockLabResultsRepository.Object,
                    _mockPatientService.Object,
                    _mockLogger.Object
                );
        }

        #region SoftDeleteLabResultAsync

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        public void SoftDeleteLabResultAsync_ThrowsArgumentException_WhenInvalidIdIsPassed(string? id)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await underTest.SoftDeleteLabResultAsync(id));
            Assert.That(exception.Message, Does.Contain("id"));
        }

        [Test]
        public void SoftDeleteLabResultAsync_ThrowsArgumentNullException_WhenInvalidIdIsPassed()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await underTest.SoftDeleteLabResultAsync(null));
            Assert.That(exception.Message, Does.Contain("id"));
        }

        [Test]
        [TestCase("abcdef")]
        [TestCase("00000000-0000-0000-0000-000000000000")]
        public void SoftDeleteLabResultAsync_ThrowsInvalidDataException_WhenRandomIdIsPassed(string id)
        {
            var exception = Assert.ThrowsAsync<InvalidDataException>(() => underTest.SoftDeleteLabResultAsync(id));
            Assert.That(exception.Message, Does.Contain($"{id}"));
        }

        [Test]
        public void SoftDeleteLabResultAsync_ThrowsResourceNotFoundException_WhenTheRecordDoesntExists()
        {
            var id = _fixture.Create<Guid>();
            _mockLabResultsRepository.
                Setup(repository => repository.GetLabResultsByIdAsync(id))
                .ReturnsAsync((LabResult?)null);

            var exception = Assert.ThrowsAsync<ResourceNotFoundException>(() => underTest.SoftDeleteLabResultAsync(id.ToString()));
            Assert.That(exception.Message, Does.Contain(id.ToString()));
        }

        [Test]
        public async Task SoftDeleteLabResultAsync_InvokesRepositoryWithUpdatedRecord_WhenTheRecordDoesntExists()
        {
            var id = _fixture.Create<Guid>();
            var labResultRecord = _fixture.Build<LabResult>()
                .With(x => x.IsActive, true)
                .Create();

            _mockLabResultsRepository
                .Setup(repository => repository.GetLabResultsByIdAsync(id))
                .ReturnsAsync(labResultRecord);

            _mockLabResultsRepository
                .Setup(repository => repository.UpdateLabResultAsync(labResultRecord))
                .ReturnsAsync(true);

            var result = await underTest.SoftDeleteLabResultAsync(id.ToString());

            Assert.That(result, Is.True);
            labResultRecord.IsActive = false;
            _mockLabResultsRepository.Verify( e=> e.UpdateLabResultAsync(labResultRecord), Times.Once);
        }
        #endregion

        #region FilterLabResultsByPatientIdAsync
        [Test]
        [TestCase("")]
        [TestCase(" ")]
        public void FilterLabResultsByPatientIdAsync_ThrowsArgumentException_WhenInvalidIdIsPassed(string? patientId)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await underTest.FilterLabResultsByPatientIdAsync(patientId));
            Assert.That(exception.Message, Does.Contain("patientId"));
        }

        [Test]
        public void FilterLabResultsByPatientIdAsync_ThrowsArgumentNullException_WhenInvalidIdIsPassed()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await underTest.FilterLabResultsByPatientIdAsync(null));
            Assert.That(exception.Message, Does.Contain("patientId"));
        }

        [Test]
        [TestCase("abcdef")]
        [TestCase("00000000-0000-0000-0000-000000000000")]
        [TestCase("{00000000-0000-0000-0000-000000000000}")]
        public void FilterLabResultsByPatientIdAsync_ThrowsInvalidDataException_WhenRandomIdIsPassed(string patientId)
        {
            var exception = Assert.ThrowsAsync<InvalidDataException>(() => underTest.FilterLabResultsByPatientIdAsync(patientId));
            Assert.That(exception.Message, Does.Contain($"{patientId}"));
        }

        [Test]
        public void FilterLabResultsByPatientIdAsync_ThrowsResourceNotFoundException_WhenThePatientRecordDoesntExists()
        {
            var id = _fixture.Create<Guid>().ToString();
            _mockPatientService.
                Setup(service => service.IsAValidUserAsync(id))
                .ReturnsAsync(false);

            var exception = Assert.ThrowsAsync<ResourceNotFoundException>(() => underTest.FilterLabResultsByPatientIdAsync(id));
            Assert.That(exception.Message, Does.Contain(id.ToString()));
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task FilterLabResultsByPatientIdAsync_ReturnsAppropriateLabResults_WhenThePatientRecordExists(bool includeDeletedRecords)
        {
            var patientId = _fixture.Create<Guid>();
            var patientLabResults = _fixture.CreateMany<LabResult>(10);
            var expectedResult = includeDeletedRecords ? patientLabResults : patientLabResults.Where(rec => rec.IsActive).ToList();

            _mockPatientService.
                Setup(service => service.IsAValidUserAsync(patientId.ToString()))
                .ReturnsAsync(true);

            _mockLabResultsRepository
                .Setup( repo => repo.GetLabResultsByPatientIdAsync(patientId))
                .ReturnsAsync(patientLabResults);

            var actualResult = await underTest.FilterLabResultsByPatientIdAsync(patientId.ToString(), includeDeletedRecords);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task FilterLabResultsByPatientIdAsync_ReturnsEmptyListWhenUserHasNoRecords_WhenThePatientRecordExists(bool includeDeletedRecords)
        {
            var patientId = _fixture.Create<Guid>();
            var patientLabResults = _fixture.CreateMany<LabResult>(0);

            _mockPatientService.
                Setup(service => service.IsAValidUserAsync(patientId.ToString()))
                .ReturnsAsync(true);

            _mockLabResultsRepository
                .Setup(repo => repo.GetLabResultsByPatientIdAsync(patientId))
                .ReturnsAsync(patientLabResults);

            var actualResult = await underTest.FilterLabResultsByPatientIdAsync(patientId.ToString(), includeDeletedRecords);
            Assert.That(actualResult.Count(), Is.EqualTo(0));
        }
        #endregion
    }
}
