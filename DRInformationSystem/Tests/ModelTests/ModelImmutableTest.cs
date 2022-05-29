using System;
using System.Collections.Generic;
using DataAccessLayer.Models;
using NUnit.Framework;

namespace Tests.ModelTests;

[TestFixture]
public class ModelImmutableTest
{
	[Test]
	public void AggregatorModelImmutableTest()
	{
		const long expectedId = 0;
		const string expectedLogin = "login";
		const string expectedPasswordHash = "password";

		var aggregatorModel = new AggregatorModel
		{
			Id = expectedId,
			Login = expectedLogin,
			PasswordHash = expectedPasswordHash
		};

		Assert.AreEqual(expectedId, aggregatorModel.Id);
		Assert.AreEqual(expectedLogin, aggregatorModel.Login);
		Assert.AreEqual(expectedPasswordHash, aggregatorModel.PasswordHash);
	}

	[Test]
	public void ConsumerModelImmutableTest()
	{
		const long expectedId = 0;
		const string expectedLogin = "login";
		const string expectedPasswordHash = "password";

		var consumerModel = new ConsumerModel
		{
			Id = expectedId,
			Login = expectedLogin,
			PasswordHash = expectedPasswordHash
		};

		Assert.AreEqual(expectedId, consumerModel.Id);
		Assert.AreEqual(expectedLogin, consumerModel.Login);
		Assert.AreEqual(expectedPasswordHash, consumerModel.PasswordHash);
	}

	[Test]
	public void InviteModelImmutableTest()
	{
		const long expectedId = 0;
		const string expectedCode = "code";
		const bool expectedIsActivated = false;
		const long expectedAggregatorId = 0;
		var expectedAggregatorModel = new AggregatorModel
		{
			Id = expectedAggregatorId,
			Login = "login",
			PasswordHash = "password"
		};

		var inviteModel = new InviteModel
		{
			Id = expectedId,
			Code = expectedCode,
			IsActivated = expectedIsActivated,
			AggregatorId = expectedAggregatorId,
			Aggregator = expectedAggregatorModel
		};

		Assert.AreEqual(expectedId, inviteModel.Id);
		Assert.AreEqual(expectedCode, inviteModel.Code);
		Assert.AreEqual(expectedIsActivated, inviteModel.IsActivated);
		Assert.AreEqual(expectedAggregatorId, inviteModel.AggregatorId);
		Assert.AreEqual(expectedAggregatorModel, inviteModel.Aggregator);
	}

	[Test]
	public void OrderModelImmutableTest()
	{
		const long expectedId = 0;
		var expectedStartTimestamp = new DateTime(2022, 5, 20, 10, 10, 10);
		var expectedEndTimestamp = new DateTime(2022, 5, 20, 10, 10, 20);
		const OrderState expectedState = OrderState.NotFormatted;
		const long expectedAggregatorId = 0;
		var expectedAggregatorModel = new AggregatorModel
		{
			Id = expectedAggregatorId,
			Login = "login",
			PasswordHash = "password"
		};

		var expectedResponseList = new List<ResponseModel>();
		var expectedSheddingList = new List<SheddingModel>();

		var order = new OrderModel
		{
			Id = expectedId,
			StartTimestamp = expectedStartTimestamp,
			EndTimestamp = expectedEndTimestamp,
			State = expectedState,
			AggregatorId = expectedAggregatorId,
			Aggregator = expectedAggregatorModel,
			Sheddings = expectedSheddingList,
			Responses = expectedResponseList
		};

		Assert.AreEqual(expectedId, order.Id);
		Assert.AreEqual(expectedStartTimestamp, order.StartTimestamp);
		Assert.AreEqual(expectedEndTimestamp, order.EndTimestamp);
		Assert.AreEqual(expectedState, order.State);
		Assert.AreEqual(expectedAggregatorId, order.AggregatorId);
		Assert.AreEqual(expectedAggregatorModel, order.Aggregator);
		Assert.AreEqual(expectedResponseList, order.Responses);
		Assert.AreEqual(expectedSheddingList, order.Sheddings);
	}

	[Test]
	public void OrderSearchParamsImmutableTest()
	{
		const long expectedAggregatorId = 0;
		const long expectedConsumerId = 0;

		var orderSearchParams = new OrderSearchParams
		{
			AggregatorId = expectedAggregatorId,
			ConsumerId = expectedConsumerId
		};

		Assert.AreEqual(expectedAggregatorId, orderSearchParams.AggregatorId.Value);
		Assert.AreEqual(expectedConsumerId, orderSearchParams.ConsumerId.Value);
	}

	[Test]
	public void ResponseModelImmutableTest()
	{
		const long expectedId = 0;
		var expectedReduceData = new List<long>
			{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24};

		const long expectedOrderId = 0;
		var expectedOrderModel = new OrderModel
		{
			Id = expectedOrderId,
		};

		const long expectedConsumerId = 0;
		var expectedConsumer = new ConsumerModel
		{
			Id = expectedConsumerId
		};

		var response = new ResponseModel
		{
			Id = expectedId,
			ReduceData = expectedReduceData,
			OrderId = expectedOrderId,
			Order = expectedOrderModel,
			ConsumerId = expectedConsumerId,
			Consumer = expectedConsumer
		};

		Assert.AreEqual(expectedId, response.Id);
		Assert.AreEqual(expectedReduceData, response.ReduceData);
		Assert.AreEqual(expectedOrderId, response.OrderId);
		Assert.AreEqual(expectedOrderModel, response.Order);
		Assert.AreEqual(expectedConsumerId, response.ConsumerId);
		Assert.AreEqual(expectedConsumer, response.Consumer);
	}

	[Test]
	public void ResponseSearchParamsImmutableTest()
	{
		const long expectedOrderId = 0;
		const long expectedConsumerId = 0;

		var responseSearchParams = new ResponseSearchParams
		{
			OrderId = expectedOrderId,
			ConsumerId = expectedConsumerId
		};

		Assert.AreEqual(expectedOrderId, responseSearchParams.OrderId.Value);
		Assert.AreEqual(expectedConsumerId, responseSearchParams.ConsumerId.Value);
	}

	[Test]
	public void SheddingModelImmutableTest()
	{
		const long expectedId = 0;
		var expectedStartTimestamp = new DateTime(2022, 5, 20, 10, 10, 20);
		const long expectedDuration = 2;
		const long expectedVolume = 0;
		const SheddingState expectedStatus = SheddingState.Approved;
		const long expectedOrderId = 0;
		var expectedOrderModel = new OrderModel
		{
			Id = expectedOrderId,
		};

		const long expectedConsumerId = 0;
		var expectedConsumer = new ConsumerModel
		{
			Id = expectedConsumerId
		};

		var shedding = new SheddingModel
		{
			Id = expectedId,
			StartTimestamp = expectedStartTimestamp,
			Duration = expectedDuration,
			Volume = expectedVolume,
			Status = expectedStatus,
			OrderId = expectedOrderId,
			Order = expectedOrderModel,
			ConsumerId = expectedConsumerId,
			Consumer = expectedConsumer
		};

		Assert.AreEqual(expectedId, shedding.Id);
		Assert.AreEqual(expectedStartTimestamp, shedding.StartTimestamp);
		Assert.AreEqual(expectedDuration, shedding.Duration);
		Assert.AreEqual(expectedVolume, shedding.Volume);
		Assert.AreEqual(expectedStatus, shedding.Status);
		Assert.AreEqual(expectedOrderId, shedding.OrderId);
		Assert.AreEqual(expectedOrderModel, shedding.Order);
		Assert.AreEqual(expectedConsumerId, shedding.ConsumerId);
		Assert.AreEqual(expectedConsumer, shedding.Consumer);
	}

	[Test]
	public void SheddingSearchParamsImmutableTest()
	{
		const long expectedOrderId = 0;
		const long expectedConsumerId = 0;

		var sheddingSearchParams = new SheddingSearchParams
		{
			OrderId = expectedConsumerId,
			ConsumerId = expectedConsumerId
		};

		Assert.AreEqual(expectedOrderId, sheddingSearchParams.OrderId.Value);
		Assert.AreEqual(expectedConsumerId, sheddingSearchParams.ConsumerId.Value);
	}
}