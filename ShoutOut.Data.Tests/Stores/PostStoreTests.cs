using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoutOut.Data.Entities;
using ShoutOut.Data.Stores;
using ShoutOut.Domain.Models;
using ShoutOut.Domain.Repositories;
using ShoutOut.Service.Mappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShoutOut.Data.Tests
{
	[TestClass]
	public class PostStoreTests
	{
		private IMapper mapper;
		private ICollection<EntityPost> internalStore;

		#region Test methods

		[TestInitialize]
		public void PostStoreTestsInit()
		{
			mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
			internalStore = new Collection<EntityPost>();

			CreatePosts();
		}

		[TestCleanup]
		public void PostStoreTestsCleanUp()
		{
			mapper = null;
			internalStore = null;
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostStoreNewInstanceTest_WhenPostMapperParameterIsNull_ReturnsArgumentNullException()
		{
			new PostStore(null);
		}

		[TestMethod]
		public void PostStoreNewInstanceTest_WhenAllParametersAreValid_ReturnsInstanceOfPostStore()
		{
			IPostRepository sut = new PostStore(mapper);

			Assert.IsInstanceOfType(sut, typeof(PostStore));
		}

		[TestMethod]
		public void PostStoreGetAllTest_ReturnsAllPosts()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			ICollection<Post> result = sut.GetAll();

			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void PostStoreGetAllTest_ReturnsEmptyCollectionOfPosts()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			
			ClearInternalStore();

			ICollection<Post> result = sut.GetAll();

			Assert.IsTrue(result.Count == 0);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void PostStoreGetTest_IfPostDoesNotExist_ReturnsInvalidOperationException()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			Post result = sut.Get(Guid.NewGuid().ToString("N"), "captainamericaatmaildotcom");
		}

		[TestMethod]
		public void PostStoreGetTest_ReturnsPost()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			Post result = sut.Get(Guid.Empty.ToString("N"), "batmanatmaildotcom");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostStoreCreateTest_WhenPostIsNull_ReturnsArgumentNullException()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			sut.Create(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostStoreUpdateTest_WhenIdParameterIsNull_ReturnsArgumentNullException()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			sut.Update(null, "batmanatmaildotcom", CreateDomainModelPost());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostStoreUpdateTest_WhenAuthorIdParameterIsNull_ReturnsArgumentNullException()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			sut.Update(Guid.Empty.ToString("N"), null, CreateDomainModelPost());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostStoreUpdateTest_WhenPostIsNull_ReturnsArgumentNullException()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			sut.Update(Guid.Empty.ToString("N"), "batmanatmaildotcom", null);
		}

		[TestMethod]
		public void PostStoreUpdateTest_WhenAllParametersAreValid_UpdatesPostInStore()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			sut.Update(Guid.Empty.ToString("N"), "batmanatmaildotcom", CreateDomainModelPost());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostStoreDeleteTest_WhenIdParameterIsNull_ReturnsArgumentNullException()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			sut.Delete(null, "batmanatmaildotcom");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostStoreDeleteTest_WhenAuthorIdParameterIsNull_ReturnsArgumentNullException()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			sut.Delete(Guid.Empty.ToString("N"), null);
		}

		[TestMethod]
		public void PostStoreDeleteTest_WhenAllParametersAreValid_DeletesPostFromStore()
		{
			IPostRepository sut = new PostStore(mapper, internalStore);
			sut.Delete(Guid.Empty.ToString("N"), "batmanatmaildotcom");
		}

		#endregion

		#region Private methods

		private void ClearInternalStore()
		{
			internalStore.Clear();
		}

		private Post CreateDomainModelPost()
		{
			return new Post(
				Guid.Empty.ToString("N"),
				"batmanatmaildotcom",
				"Bruce Wayne",
				"Batman",
				"Bruce Wayne is batman.",
				DateTime.Now,
				DateTime.Now
			);
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

		#endregion
	}
}
