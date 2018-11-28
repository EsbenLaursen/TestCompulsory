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
    public class GetFullyOccupiedDatesSteps
    {

        private IBookingManager bookingManager;
        private DateTime Start;
        private DateTime End;

        private IRepository<Booking> bookingRepository;
        private IRepository<Room> roomRepository;

        [Given(@"I have typed '(.*)' and '(.*)'")]
        public void GivenIHaveTypedAnd(string p0, string p1)
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
        
        [Given(@"the room from '(.*)' to '(.*)'")]
        public void GivenTheRoomFromTo(string p0, string p1)
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
        
        [When(@"I press buttonr")]
        public void WhenIPressButtonr()
        {
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }
        
        [Then(@"the resultus shokuld be '(.*)'")]
        public void ThenTheResultusShokuldBe(int p0)
        {
            var result = bookingManager.GetFullyOccupiedDates(Start, End);

            Assert.Equal(result.Count, p0);
        }
    }
}
