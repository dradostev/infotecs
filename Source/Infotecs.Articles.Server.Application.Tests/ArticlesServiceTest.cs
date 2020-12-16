using System;
using System.Threading;
using FluentValidation;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Core.Testing;
using Grpc.Core.Utils;
using Infotecs.Articles.Server.Application.Services;
using Infotecs.Articles.Server.Application.Validators;
using Infotecs.Articles.Server.Domain.Entities;
using Infotecs.Articles.Server.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Infotecs.Articles.Server.Application.Tests
{
    /// <summary>
    /// Unit tests for Articles gRPC service.
    /// </summary>
    public class ArticlesServiceTest
    {
        /// <summary>
        /// Test article creation.
        /// </summary>
        [Fact]
        public void CreateArticle_ReceivesCreateArticleRequest_ExpectsArticleModel()
        {
            var username = "user1";
            var title = "My Article 1";
            var content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            var thumbnail = new byte[] { 72, 101, 108, 108, 111 };

            var newArticle = new Article(
                username,
                title,
                content,
                thumbnail);

            var mockArticlesRepo = new Mock<IArticlesRepository>();
            mockArticlesRepo
                .Setup(x => x.CreateAsync(It.IsAny<Article>()))
                .ReturnsAsync(newArticle);

            var mockCommentsRepo = new Mock<ICommentsRepository>();

            var mockLogger = new Mock<ILogger<ArticlesService>>();

            var mockValidationFactory = new Mock<IValidatorFactory>();

            mockValidationFactory.Setup(x =>
                x.GetValidator<CreateArticleRequest>()).Returns(new CreateArticleRequestValidator());

            var mockContext = GetServerCallContext("CreateArticle");

            var service = new ArticlesService(
                mockLogger.Object,
                mockArticlesRepo.Object,
                mockCommentsRepo.Object,
                mockValidationFactory.Object);

            var request = new CreateArticleRequest
            {
                User = username,
                Title = title,
                Content = content,
                ThumbnailImage = ByteString.CopyFrom(thumbnail),
            };

            var result = service.CreateArticle(request, mockContext).Result;

            Assert.Equal(result.User, request.User);
            Assert.Equal(result.Title, request.Title);
            Assert.Equal(result.Content, request.Content);
            Assert.Equal(result.ThumbnailImage, request.ThumbnailImage);
        }

        /// <summary>
        /// Test article list retrieving.
        /// </summary>
        [Fact]
        public void ListArticle_ReceivesEmptyRequest_ExpectsArticleModelsEnumerable()
        {
            var mockArticlesRepo = new Mock<IArticlesRepository>();
            mockArticlesRepo
                .Setup(x => x.ListAsync())
                .ReturnsAsync(new []
                {
                    new Article(
                        "user1",
                        "My Article 1",
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                        new byte[] { 72, 101, 108, 108, 111 }),
                    new Article(
                        "user1",
                        "My Article 2",
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                        new byte[] { 72, 101, 108, 108, 111 }),
                    new Article(
                        "user2",
                        "My Article 3",
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                        new byte[] { 72, 101, 108, 108, 111 }),
                });

            var mockCommentsRepo = new Mock<ICommentsRepository>();

            var mockLogger = new Mock<ILogger<ArticlesService>>();

            var mockValidationFactory = new Mock<IValidatorFactory>();

            mockValidationFactory.Setup(x =>
                x.GetValidator<CreateArticleRequest>()).Returns(new CreateArticleRequestValidator());

            var mockContext = GetServerCallContext("ListArticles");

            var service = new ArticlesService(
                mockLogger.Object,
                mockArticlesRepo.Object,
                mockCommentsRepo.Object,
                mockValidationFactory.Object);

            var request = new EmptyRequest();

            var result = service.ListArticles(request, mockContext).Result;

            Assert.NotEmpty(result.Articles);
            Assert.True(result.Articles.Count == 3);
            Assert.Collection(
                result.Articles,
                x => Assert.Contains("My Article 1", x.Title),
                x => Assert.Contains("My Article 2", x.Title),
                x => Assert.Contains("My Article 3", x.Title));
        }

        /// <summary>
        /// Test article retrieving.
        /// </summary>
        [Fact]
        public void ShowArticle_ReceivesShowArticleRequest_ExpectsArticleModel()
        {
            var username = "user1";
            var title = "My Article 1";
            var content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            var thumbnail = new byte[] { 72, 101, 108, 108, 111 };

            var article = new Article(
                username,
                title,
                content,
                thumbnail);

            var mockArticlesRepo = new Mock<IArticlesRepository>();
            mockArticlesRepo.Setup(x => x.ShowAsync(3)).ReturnsAsync(article);

            var mockCommentsRepo = new Mock<ICommentsRepository>();

            var mockLogger = new Mock<ILogger<ArticlesService>>();

            var mockValidationFactory = new Mock<IValidatorFactory>();

            mockValidationFactory.Setup(x =>
                x.GetValidator<CreateArticleRequest>()).Returns(new CreateArticleRequestValidator());

            var mockContext = GetServerCallContext("ShowArticle");

            var service = new ArticlesService(
                mockLogger.Object,
                mockArticlesRepo.Object,
                mockCommentsRepo.Object,
                mockValidationFactory.Object);

            var result = service.ShowArticle(new ShowArticleRequest { ArticleId = 3 }, mockContext).Result;

            Assert.Equal(result.Article.User, username);
            Assert.Equal(result.Article.Title, title);
            Assert.Equal(result.Article.Content, content);
            Assert.Equal(result.Article.ThumbnailImage, thumbnail);
        }

        /// <summary>
        /// Test article deletion.
        /// </summary>
        [Fact]
        public void DeleteArticle_ReceivesDeleteArticleRequest_ExpectsEmptyReply()
        {
            var username = "user1";
            var title = "My Article 1";
            var content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            var thumbnail = new byte[] { 72, 101, 108, 108, 111 };

            var article = new Article(
                username,
                title,
                content,
                thumbnail);

            var mockArticlesRepo = new Mock<IArticlesRepository>();
            mockArticlesRepo.Setup(x => x.DeleteAsync(3)).ReturnsAsync(true);

            var mockCommentsRepo = new Mock<ICommentsRepository>();

            var mockLogger = new Mock<ILogger<ArticlesService>>();

            var mockValidationFactory = new Mock<IValidatorFactory>();

            mockValidationFactory.Setup(x =>
                x.GetValidator<AddCommentRequest>()).Returns(new AddCommentRequestValidator());

            var mockContext = GetServerCallContext("DeleteArticle");

            var service = new ArticlesService(
                mockLogger.Object,
                mockArticlesRepo.Object,
                mockCommentsRepo.Object,
                mockValidationFactory.Object);

            var result = service.DeleteArticle(new DeleteArticleRequest { ArticleId = 3 }, mockContext).Result;

            Assert.IsType<EmptyReply>(result);
        }

        /// <summary>
        /// Test comment adding.
        /// </summary>
        [Fact]
        public void AddComment_ReceivesAddCommentRequest_ExpectsCommentModel()
        {
            var username = "user1";
            var title = "My Article 1";
            var content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            var thumbnail = new byte[] { 72, 101, 108, 108, 111 };

            var article = new Article(
                username,
                title,
                content,
                thumbnail);

            var comment = new Comment(3, username, "Blahblhablah");

            var mockArticlesRepo = new Mock<IArticlesRepository>();

            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo
                .Setup(x => x.CreateAsync(It.IsAny<Comment>()))
                .ReturnsAsync(comment);

            var mockLogger = new Mock<ILogger<ArticlesService>>();

            var mockValidationFactory = new Mock<IValidatorFactory>();

            mockValidationFactory.Setup(x =>
                x.GetValidator<AddCommentRequest>()).Returns(new AddCommentRequestValidator());

            var mockContext = GetServerCallContext("AddComment");

            var service = new ArticlesService(
                mockLogger.Object,
                mockArticlesRepo.Object,
                mockCommentsRepo.Object,
                mockValidationFactory.Object);

            var request = new AddCommentRequest
            {
                ArticleId = 3,
                Content = comment.Content,
                User = comment.Username,
            };

            var result = service.AddComment(request, mockContext).Result;

            Assert.Equal(result.ArticleId, request.ArticleId);
            Assert.Equal(result.User, request.User);
            Assert.Equal(result.Content, request.Content);
        }

        private static ServerCallContext GetServerCallContext(string method) => TestServerCallContext.Create(
            method,
            "127.0.0.1",
            DateTime.UtcNow.AddHours(1),
            new Metadata(),
            CancellationToken.None,
            "127.0.0.1",
            null,
            null,
            x => TaskUtils.CompletedTask,
            () => new WriteOptions(),
            x => { });
    }
}
