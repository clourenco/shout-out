using AutoMapper;
using ShoutOut.Service.Dtos.Models;
using System;
using ShoutOut.Domain.Models;
using ShoutOut.Data.Entities;

namespace ShoutOut.Service.Mappers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<PostCreateDto, Post>().ConstructUsing(p => new Post(Guid.NewGuid().ToString("N"), p.AuthorId.Replace("@", "at").Replace(".", "dot"), p.Author, p.Title, p.Message, DateTime.Now, DateTime.Now));

			CreateMap<PostUpdateDto, Post>().ConstructUsing(p => new Post(Guid.Empty.ToString("N"), "", "", p.Title, p.Message, null, DateTime.Now));

			CreateMap<EntityPost, Post>()
				.ConstructUsing(p => new Post(p.Id, p.AuthorId, p.Author, p.Title, p.Message, p.Created, p.Updated));
		}
	}
}
