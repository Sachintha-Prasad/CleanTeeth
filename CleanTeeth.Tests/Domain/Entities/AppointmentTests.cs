using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.Entities
{
    [TestClass]
    public class AppointmentTests
    {
        private Guid _patientId = Guid.NewGuid();
        private Guid _dentistId = Guid.NewGuid();
        private Guid _dentalOfficeId = Guid.NewGuid();
        private TimeInterval _timeInterval = new TimeInterval(DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2));

        [TestMethod]
        public void Constructor_ValidAppointment_ShouldSetScheduled()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _timeInterval);

            Assert.AreEqual(_patientId, appointment.PatientId);
            Assert.AreEqual(_dentistId, appointment.DentistId);
            Assert.AreEqual(_dentalOfficeId, appointment.DentalOfficeId);
            Assert.AreEqual(_timeInterval, appointment.TimeInterval);
            Assert.AreEqual(AppointmentStatus.Scheduled, appointment.Status);
            Assert.AreNotEqual(Guid.Empty, appointment.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Constructor_StartTimeInThePast_ShouldThrow()
        {
            var interval = new TimeInterval(DateTime.UtcNow.AddHours(-1), DateTime.UtcNow);

            new Appointment(_patientId, _dentistId, _dentalOfficeId, interval);
        }

        [TestMethod]
        public void Cancel_WhenScheduled_ShouldSetCancelled()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _timeInterval);

            appointment.Cancel();

            Assert.AreEqual(AppointmentStatus.Canceled, appointment.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Cancel_WhenAlreadyCancelled_ShouldThrow()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _timeInterval);

            appointment.Cancel();
            appointment.Cancel();
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Cancel_WhenCompleted_ShouldThrow()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _timeInterval);

            appointment.Complete();
            appointment.Cancel();
        }

        [TestMethod]
        public void Complete_WhenScheduled_ShouldSetCompleted()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _timeInterval);

            appointment.Complete();

            Assert.AreEqual(AppointmentStatus.Completed, appointment.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Complete_WhenAlreadyCompleted_ShouldThrow()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _timeInterval);

            appointment.Complete();
            appointment.Complete();
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessRuleException))]
        public void Complete_WhenCancelled_ShouldThrow()
        {
            var appointment = new Appointment(_patientId, _dentistId, _dentalOfficeId, _timeInterval);

            appointment.Cancel();
            appointment.Complete();
        }
    }
}