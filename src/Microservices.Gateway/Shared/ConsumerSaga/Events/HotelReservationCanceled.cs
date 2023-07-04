﻿using MassTransit;

namespace Microservices.Gateway.Shared.ConsumerSaga.Events;
public class HotelReservationCanceled : CorrelatedBy<Guid>
{
  public Guid CorrelationId { get; set; }
  public Guid ReservationId { get; set; }
}