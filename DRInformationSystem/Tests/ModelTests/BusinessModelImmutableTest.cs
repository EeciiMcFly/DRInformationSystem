using System;
using System.Collections.Generic;
using BusinessLogicLayer.Models;
using NUnit.Framework;

namespace Tests.ModelTests;

[TestFixture]
public class BusinessModelImmutableTest
{
	[Test]
	public void CreateOrderDataImmutableTest()
	{
		var expectedPeriodStart = new DateTime(2022, 5, 20, 10, 10, 10);
		var expectedPeriodEnd = new DateTime(2022, 5, 20, 10, 10, 20);
		const long expectedAggregatorId = 0;

		var createOrderData = new CreateOrderData
		{
			PeriodStart = expectedPeriodStart,
			PeriodEnd = expectedPeriodEnd,
			AggregatorId = expectedAggregatorId
		};

		Assert.AreEqual(expectedPeriodStart, createOrderData.PeriodStart);
		Assert.AreEqual(expectedPeriodEnd, createOrderData.PeriodEnd);
		Assert.AreEqual(expectedAggregatorId, createOrderData.AggregatorId);
	}

	[Test]
	public void CreateResponseDataImmutableTest()
	{
		const long expectedConsumerId = 0;
		const long expectedOrderId = 0;
		var expectedReduceData = new List<long>
			{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24};

		var createResponseData = new CreateResponseData
		{
			ConsumerId = expectedConsumerId,
			OrderId = expectedOrderId,
			ReduceData = expectedReduceData
		};

		Assert.AreEqual(expectedConsumerId, createResponseData.ConsumerId);
		Assert.AreEqual(expectedOrderId, createResponseData.OrderId);
		Assert.AreEqual(expectedReduceData, createResponseData.ReduceData);
	}

	[Test]
	public void CreateSheddingDataImmutableTest()
	{
		var expectedStartTimestamp = new DateTime(2022, 5, 20, 10, 10, 20);
		const long expectedDuration = 2;
		const long expectedVolume = 0;
		const long expectedOrderId = 0;
		const long expectedConsumerId = 0;

		var createSheddingData = new CreateSheddingData
		{
			StartTime = expectedStartTimestamp,
			Duration = expectedDuration,
			Volume = expectedVolume,
			OrderId = expectedOrderId,
			ConsumerId = expectedConsumerId
		};

		Assert.AreEqual(expectedStartTimestamp, createSheddingData.StartTime);
		Assert.AreEqual(expectedDuration, createSheddingData.Duration);
		Assert.AreEqual(expectedVolume, createSheddingData.Volume);
		Assert.AreEqual(expectedOrderId, createSheddingData.OrderId);
		Assert.AreEqual(expectedConsumerId, createSheddingData.ConsumerId);
	}

	[Test]
	public void RegisterConsumerDataImmutableTest()
	{
		const string expectedLogin = "login";
		const string expectedPassword = "password";
		const string expectedInviteCode = "code";

		var registerConsumerData = new RegisterConsumerData
		{
			Login = expectedLogin,
			Password = expectedPassword,
			InviteCode = expectedInviteCode
		};

		Assert.AreEqual(expectedLogin, registerConsumerData.Login);
		Assert.AreEqual(expectedPassword, registerConsumerData.Password);
		Assert.AreEqual(expectedInviteCode, registerConsumerData.InviteCode);
	}
}