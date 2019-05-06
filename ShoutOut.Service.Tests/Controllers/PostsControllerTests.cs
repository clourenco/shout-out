using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoutOut.Controllers;
using ShoutOut.Data.Entities;
using ShoutOut.Data.Stores;
using ShoutOut.Domain.Repositories;
using ShoutOut.Service.Mappers;
using System;
using System.Collections.Generic;

namespace ShoutOut.Service.Tests
{
	[TestClass]
	public class PostsControllerTests
	{
		private IMapper mapper;
		private IPostRepository store;
		private ICollection<EntityPost> internalStore;

		[TestInitialize]
		public void PostsControllerTestsInit()
		{
			mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));

			CreatePosts();

			store = new PostStore(mapper, internalStore);
		}

		[TestCleanup]
		public void PostsControllerTestsCleanUp()
		{
			
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostsControllerNewInstanceTest_WhenPostMapperParameterIsNull_ReturnsArgumentNullException()
		{
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostsControllerNewInstanceTest_WhenPostRepositoryParameterIsNull_ReturnsArgumentNullException()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void PostsControllerGetAllTest_ReturnsCollectionOfPostsResponse()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void PostsControllerGetItemTest_ReturnsSinglePostResponse()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void PostsControllerPostTest_ReturnsSinglePostResponse()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void PostsControllerPutTest_ReturnsSinglePostResponse()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void PostsControllerDeleteTest_ReturnsSimplePostResponse()
		{
			//PostsController sut = new PostsController(store, mapper);
			//sut.Delete(Guid.Empty.ToString("N"), "batmanatmaildotcom");
			Assert.Fail();
		}

		private void CreatePosts()
		{
			EntityPost itemToAdd = new EntityPost();

			itemToAdd.Id = Guid.Empty.ToString("N");
			itemToAdd.AuthorId = "batmanatmaildotcom";
			itemToAdd.Author = "Bruce Wayne";
			itemToAdd.Title = "Batman";
			itemToAdd.Message = "Bruce Wayne is batman.";
			itemToAdd.Created = DateTime.Now;
			itemToAdd.Updated = DateTime.Now;

			internalStore.Add(itemToAdd);

			itemToAdd = new EntityPost();

			itemToAdd.Id = Guid.NewGuid().ToString("N");
			itemToAdd.AuthorId = "ironmanatmaildotcom";
			itemToAdd.Author = "Tony Stark";
			itemToAdd.Title = "Ironman";
			itemToAdd.Message = "Tony Stark is ironman.";
			itemToAdd.Created = DateTime.Now;
			itemToAdd.Updated = DateTime.Now;

			internalStore.Add(itemToAdd);

			itemToAdd = new EntityPost();

			itemToAdd.Id = Guid.NewGuid().ToString("N");
			itemToAdd.AuthorId = "spidermanatmaildotcom";
			itemToAdd.Author = "Peter Parker";
			itemToAdd.Title = "Spiderman";
			itemToAdd.Message = "Peter Parker is spiderman.";
			itemToAdd.Created = DateTime.Now;
			itemToAdd.Updated = DateTime.Now;

			internalStore.Add(itemToAdd);

			itemToAdd = new EntityPost();

			itemToAdd.Id = Guid.NewGuid().ToString("N");
			itemToAdd.AuthorId = "hulkatmaildotcom";
			itemToAdd.Author = "Bruce Banner";
			itemToAdd.Title = "Hulk";
			itemToAdd.Message = "Bruce Banner is hulk.";
			itemToAdd.Created = DateTime.Now;
			itemToAdd.Updated = DateTime.Now;

			internalStore.Add(itemToAdd);
		}
	}
}
