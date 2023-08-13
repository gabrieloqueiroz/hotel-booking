using Domain.Guest.Enums;
using Domain.Booking;
using NUnit.Framework;
using Domain.Booking.Entities;

namespace DomainTests.Entities;

public class BookingTest
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldAlwaysStartWithCreatedStatus()
    {
        // Given - When
        var booking = new BookingEntity();

        // Then
        Assert.AreEqual(EStatus.Created, booking.CurrentStatus);
    }

    [Test]
    public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
    {
        // Given
        var booking = new BookingEntity();

        // When
        booking.ChangeState(EAction.Pay);

        // Then
        Assert.AreEqual(EStatus.Paid, booking.CurrentStatus);
    }


    [Test]
    public void ShouldSetStatusToCanceledWhenCancelingABookingWithCreatedStatus()
    {
        // Given
        var booking = new BookingEntity();

        // When
        booking.ChangeState(EAction.Cancel);

        // Then
        Assert.AreEqual(EStatus.Canceled, booking.CurrentStatus);
    }

    [Test]
    public void ShouldSetStatusToFinishedWhenFinishingAPaidBooking()
    {
        // Given
        var booking = new BookingEntity();

        // When
        booking.ChangeState(EAction.Pay);
        booking.ChangeState(EAction.Finish);

        // Then
        Assert.AreEqual(EStatus.Finished, booking.CurrentStatus);
    }


    [Test]
    public void ShouldSetStatusToRefoundedWhenRefoundingAPaidBooking()
    {
        // Given
        var booking = new BookingEntity();

        // When
        booking.ChangeState(EAction.Pay);
        booking.ChangeState(EAction.Refound);

        // Then
        Assert.AreEqual(EStatus.Refounded, booking.CurrentStatus);
    }

    [Test]
    public void ShouldSetStatusToCreatedWhenReopeningACanceledBooking()
    {
        // Given
        var booking = new BookingEntity();

        // When
        booking.ChangeState(EAction.Cancel);
        booking.ChangeState(EAction.Reopen);

        // Then
        Assert.AreEqual(EStatus.Created, booking.CurrentStatus);
    }

    [Test]
    public void ShouldNotSetRefoundedWhenABookingWasNotPaid()
    {
        // Given
        var booking = new BookingEntity();

        // When
        booking.ChangeState(EAction.Refound);

        // Then
        Assert.AreNotEqual(EStatus.Refounded, booking.CurrentStatus);
        Assert.AreEqual(EStatus.Created, booking.CurrentStatus);
    }

    [Test]
    public void ShouldNotSetRefoundedWhenABookingWasFinished()
    {
        // Given
        var booking = new BookingEntity();

        // When
        booking.ChangeState(EAction.Pay);
        booking.ChangeState(EAction.Finish);
        booking.ChangeState(EAction.Refound);

        // Then
        Assert.AreNotEqual(EStatus.Refounded, booking.CurrentStatus);
        Assert.AreEqual(EStatus.Finished, booking.CurrentStatus);
    }
}