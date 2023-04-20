using System;

namespace Lab.RateMyBeer.Frontend.Contracts.Checkins.Commands;

public record CommentCheckinCommandViewModel(Guid CheckinId, string Comment);