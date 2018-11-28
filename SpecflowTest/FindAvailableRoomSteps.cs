using HotelBooking.BusinessLogic;
using HotelBooking.Models;
using HotelBooking.UnitTests.Fakes;
using Moq;
using System;
using TechTalk.SpecFlow;
using Xunit;
namespace SpecflowTest
{
    [Binding]
    public class FindAvailableRoomSteps
    {
        private IBookingManager bookingManager;
        private DateTime Start;
        private DateTime End;

        private IRepository<Booking> bookingRepository;
        private IRepository<Room> roomRepository;

        [Given(@"I have entered '(.*)' and '(.*)'")]
        public void GivenIHaveEnteredAnd(string p0, string p1)
        {
            try
            {

                Start = DateTime.Parse(p0);
                End = DateTime.Parse(p1);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        
        [Given(@"the room is already booked from '(.*)' to '(.*)'")]
        public void GivenTheRoomIsAlreadyBookedFromTo(string p0, string p1)
        {
            var start = DateTime.Parse(p0);
            var end = DateTime.Parse(p1);

            Mock<IRepository<Booking>> bookingRepository = new Mock<IRepository<Booking>>();
            Mock<IRepository<Room>> roomRepository = new Mock<IRepository<Room>>();

            bookingRepository.Setup(x => x.GetAll()).Returns(new FakeBookingRepository(start, end).GetAll());
            roomRepository.Setup(x => x.GetAll()).Returns(new FakeRoomRepository().GetAll());
            bookingRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new FakeBookingRepository(start, end).Get(1));

            this.bookingRepository = bookingRepository.Object;
            this.roomRepository = roomRepository.Object;
        }
        
        [When(@"I press button")]
        public void WhenIPressButton()
        {
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }
        
        [Then(@"the resultus should be '(.*)'")]
        public void ThenTheResultusShouldBe(int p0)
        {

            var booking = new Booking()
            {
                StartDate = Start,
                EndDate = End
            };

            var result = bookingManager.FindAvailableRoom(booking.StartDate, booking.EndDate);

            Assert.Equal(result, p0);
        }
    }
}
