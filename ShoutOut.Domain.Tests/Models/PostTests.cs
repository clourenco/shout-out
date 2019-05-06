using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoutOut.Domain.Models;
using System;

namespace ShoutOut.Domain.Tests
{
	[TestClass]
	public class PostTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostNewInstanceTest_WhenIdParameterIsNull_ReturnsArgumentNullException()
		{
			new Post(null, "johndoeatmaildotcom", "John Doe", "Who's John Doe?", "I'm John Doe.", DateTime.Now, DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostNewInstanceTest_WhenAuthorIdParameterIsNull_ReturnsArgumentNullException()
		{
			new Post(Guid.NewGuid().ToString("N"), null, "John Doe", "Who's John Doe?", "I'm John Doe.", DateTime.Now, DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostNewInstanceTest_WhenAuthorParameterIsNull_ReturnsArgumentNullException()
		{
			new Post(
				Guid.NewGuid().ToString("N"), 
				"johndoeatmaildotcom", 
				null, 
				"Who's John Doe?", 
				"I'm John Doe.", 
				DateTime.Now, 
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostNewInstanceTest_WhenTitleParameterIsNull_ReturnsArgumentNullException()
		{
			new Post(
				Guid.NewGuid().ToString("N"), 
				"johndoeatmaildotcom", 
				"John Doe", 
				null, 
				"I'm John Doe.", 
				DateTime.Now, 
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PostNewInstanceTest_WhenMessageParameterIsNull_ReturnsArgumentNullException()
		{
			new Post(
				Guid.NewGuid().ToString("N"), 
				"johndoeatmaildotcom", 
				"John Doe", 
				"Who's John Doe?", 
				null, 
				DateTime.Now, 
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PostNewInstanceTest_WhenAuthorIdParameterLengthIsSmallerThanMinimumAllowed_ReturnsArgumentException()
		{
			new Post(
				Guid.NewGuid().ToString("N"), 
				"", 
				"John Doe", 
				"Who's John Doe?", 
				null, 
				DateTime.Now, 
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PostNewInstanceTest_WhenAuthorIdParameterLengthIsBiggerThanMaximumAllowed_ReturnsArgumentException()
		{
			new Post(
				Guid.NewGuid().ToString("N"),
				"johndoeatmaildotcomjohndoeatmaildotcomjohndoeatmaildotcom", 
				"John Doe", 
				"Who's John Doe?", 
				"I'm John Doe.", 
				DateTime.Now, 
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PostNewInstanceTest_WhenAuthorParameterLengthIsSmallerThanMinimumAllowed_ReturnsArgumentNullException()
		{
			new Post(
				Guid.NewGuid().ToString("N"), 
				"johndoeatmaildotcom", 
				"", 
				"Who's John Doe?",
				"I'm John Doe.", 
				DateTime.Now, 
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PostNewInstanceTest_WhenAuthorParameterLengthIsBiggerThanMaximumAllowed_ReturnsArgumentNullException()
		{
			new Post(
				Guid.NewGuid().ToString("N"), 
				"johndoeatmaildotcom",
				"John Doe John Doe John Doe John Doe John Doe John Doe John Doe", 
				"Who's John Doe?",
				"I'm John Doe.", 
				DateTime.Now, 
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PostNewInstanceTest_WhenTitleParameterLengthIsSmallerThanMinimumAllowed_ReturnsArgumentNullException()
		{
			new Post(
				Guid.NewGuid().ToString("N"),
				"johndoeatmaildotcom",
				"John Doe",
				"",
				"I'm John Doe.",
				DateTime.Now,
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PostNewInstanceTest_WhenTitleParameterLengthIsBiggerThanMaximumAllowed_ReturnsArgumentNullException()
		{
			new Post(
				Guid.NewGuid().ToString("N"),
				"johndoeatmaildotcom",
				"John Doe",
				"Who's John Doe? Who's John Doe? Who's John Doe? Who's John Doe? Who's John Doe? Who's John Doe? Who's John Doe?",
				"I'm John Doe.",
				DateTime.Now,
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PostNewInstanceTest_WhenMessageParameterLengthIsSmallerThanMinimumAllowed_ReturnsArgumentNullException()
		{
			new Post(
				Guid.NewGuid().ToString("N"),
				"johndoeatmaildotcom",
				"John Doe",
				"Who's John Doe?",
				"",
				DateTime.Now,
				DateTime.Now);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void PostNewInstanceTest_WhenMessageParameterLengthIsBiggerThanMaximumAllowed_ReturnsArgumentNullException()
		{
			new Post(
				Guid.NewGuid().ToString("N"),
				"johndoeatmaildotcom",
				"John Doe",
				"Who's John Doe?",
				"I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.I'm John Doe.",
				DateTime.Now,
				DateTime.Now);
		}

		[TestMethod]
		public void PostNewInstanceTest_WhenAllParametersAreValid_ReturnsInstanceOfPost()
		{
			Post thePost = new Post(
								Guid.NewGuid().ToString("N"),
								"johndoeatmaildotcom",
								"John Doe",
								"Who's John Doe?",
								"I'm John Doe.",
								DateTime.Now,
								DateTime.Now);

			Assert.IsInstanceOfType(thePost, typeof(Post));
		}
	}
}
