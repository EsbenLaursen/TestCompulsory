using System;
using System.Collections.Generic;
using HotelBooking.BusinessLogic;
using HotelBooking.Models;
using HotelBooking.UnitTests.Fakes;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;

        private Mock<IRepository<Booking>> fakeBookingRepository;
        private Mock<IRepository<Room>> fakeRoomRepository;

        public BookingManagerTests()
        {
            List<Room> roomList = new List<Room>() {
                new Room()
                {
                    Id=1,
                    Description="a"
                },
                new Room() {   Id=2,
                    Description="b"}
            };
            List<Booking> bookingList = new List<Booking>() {
                new Booking()
                {
                    StartDate = DateTime.Today.AddDays(10),
                    EndDate = DateTime.Today.AddDays(13),
                    RoomId = 2,
                    IsActive = true
                },
                new Booking() {
                 StartDate = DateTime.Today.AddDays(10),
                    EndDate = DateTime.Today.AddDays(13),
                    RoomId = 1,
                    IsActive = true
                }
            };

            fakeBookingRepository = new Mock<IRepository<Booking>>();
            fakeRoomRepository = new Mock<IRepository<Room>>();

            fakeBookingRepository.Setup(x => x.GetAll()).Returns(bookingList);
            fakeRoomRepository.Setup(x => x.GetAll()).Returns(roomList);

            bookingManager = new BookingManager(fakeBookingRepository.Object, fakeRoomRepository.Object);
        }

        /// <summary>
        /// GetFullyOccupiedDates tests
        /// </summary>
        [Fact]
        public void GetFullyOccupiedDates_StartDateAfterEndDate_ThrowsArgumentException()
        {
            var StartDate = DateTime.Today.AddDays(1);
              var  EndDate = DateTime.Today;
            Assert.Throws<ArgumentException>(() => bookingManager.GetFullyOccupiedDates(StartDate, EndDate));
        }

        [Fact]
        public void GetFullyOccupiedDates_NoBookings_ReturnsEmptyList()
        {
            fakeBookingRepository.Setup(x => x.GetAll()).Returns(new List<Booking>());
            var StartDate = DateTime.Today.AddDays(1);
            var EndDate = DateTime.Today.AddDays(3);
           
            var result= bookingManager.GetFullyOccupiedDates(StartDate, EndDate);
            Assert.Empty(result);
        }

        [Fact]
        public void GetFullyOccupiedDates_start9End14_ReturnsList4elements()
        {
           
            var StartDate = DateTime.Today.AddDays(9);
            var EndDate = DateTime.Today.AddDays(14);

            var result = bookingManager.GetFullyOccupiedDates(StartDate, EndDate);
            Assert.True(result.Count == 4);
        }

        // End

        /// <summary>
        /// CreateBooking tests
        /// </summary>
        [Fact]
        public void CreateBooking_BookingNull_ThrowsArgumentException()
        {
            Booking b = null;
            Assert.Throws<ArgumentException>(() => bookingManager.CreateBooking(b));
        }

        [Fact]
        public void CreateBooking_StartDateToday_ThrowsArgumentException()
        {
            Booking b = new Booking()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };
            Assert.Throws<ArgumentException>(() => bookingManager.CreateBooking(b));
        }

        [Fact]
        public void CreateBooking_StartDateTomorrowAndRoomIsAvailable_ReturnsTrue()
        {
            Booking b = new Booking()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3)
            };
            Assert.True(bookingManager.CreateBooking(b));
            fakeBookingRepository.Verify(x => x.Add(It.IsAny<Booking>()));
        }

        [Fact]
        public void CreateBooking_StartDateTomorrowAndRoomIsNotAvailable_ReturnsFalse()
        {
            Booking b = new Booking()
            {
                StartDate = DateTime.Today.AddDays(11),
                EndDate = DateTime.Today.AddDays(12)
            };
            Assert.False(bookingManager.CreateBooking(b));
            fakeBookingRepository.Verify(x => x.Add(It.IsAny<Booking>()), Times.Never);
        }

        // End


        /// <summary>
        /// FindAvailableRoom tests
        /// </summary>
        /// 
        [Fact]
        public void FindAvailableRoom_RoomNotAvailable_RoomIdMinusOne()
        {
            var StartDate = DateTime.Today.AddDays(11);
            var EndDate = DateTime.Today.AddDays(12);
            Assert.Equal(-1, bookingManager.FindAvailableRoom(StartDate, EndDate));
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            DateTime date = DateTime.Today;
            Assert.Throws<ArgumentException>(() => bookingManager.FindAvailableRoom(date, date));
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

    }
}
