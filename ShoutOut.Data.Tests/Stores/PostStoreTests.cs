using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoutOut.Data.Stores;
using ShoutOut.Domain.Repositories;
using System;

namespace ShoutOut.Data.Tests
{
	[TestClass]
	public class PostStoreTests
	{
		private IMapper mapper;

		public PostStoreTests()
		{
			mapper = new Mapper(new MapperConfiguration(new MapperConfigurationExpression()));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CreateNewInstanceOfPostStoreTest_WhenPostMapperParameterIsNull_ReturnsArgumentNullException()
		{
			new PostStore(null);
		}

		[TestMethod]
		public void CreateNewInstanceOfPostStoreTest_WhenAllParametersAreValid_ReturnsInstanceOfPostStore()
		{
			IPostRepository store = new PostStore(mapper);

			Assert.IsInstanceOfType(store, typeof(PostStore));
		}
	}
}
