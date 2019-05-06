using AutoMapper;
using ShoutOut.Service.Dtos.Models;
using System;
using ShoutOut.Domain.Models;
using ShoutOut.Data.Entities;

namespace ShoutOut.Service.Mappers
{
	public class MappingProfile : Profile
	{
		private const string DefaultAuthorId = "adminatmaildotcom";
		private const string DefaultAuthor = "Admin";
		private readonly string defaultId;

		public MappingProfile()
		{
			string id = Guid.NewGuid().ToString("N");
			defaultId = Guid.Empty.ToString("N");

			CreateMap<PostCreateDto, Post>().ConstructUsing(p => new Post(id, p.AuthorId.Replace("@", "at").Replace(".", "dot"), p.Author, p.Title, p.Message, DateTime.Now, DateTime.Now));

			CreateMap<PostUpdateDto, Post>().ConstructUsing(p => new Post(defaultId, DefaultAuthorId, DefaultAuthor, p.Title, p.Message, null, DateTime.Now));

			CreateMap<EntityPost, Post>()
				.ConstructUsing(p => new Post(p.Id, p.AuthorId, p.Author, p.Title, p.Message, p.Created, p.Updated));
		}
	}
}
