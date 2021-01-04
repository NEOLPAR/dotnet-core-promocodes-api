using Microsoft.AspNetCore.Mvc;
using Moq;
using PromocodesApp.Controllers;
using PromocodesApp.Entities;
using PromocodesApp.Interfaces;
using PromocodesApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace Tests
{
    public class CodesControllerTests
    {
        #region Code_Get_List
        [Fact]
        public async Task Get_ReturnsBadRequest_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var returning = "invalid";
            var dbMock = GetCodes(returning);
            mockRepo.Setup(x => x.Get()).Returns(dbMock);

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsEmptyListCodeDTO_GivenEmptyList()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var returning = "empty";
            var dbMock = GetCodes(returning);
            mockRepo.Setup(x => x.Get()).Returns(dbMock);

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCodes = Assert.IsType<List<CodeDTO>>(okResult.Value);
            Assert.Empty(returnCodes);
        }

        [Fact]
        public async Task Get_ReturnsListCodeDTO_GivenListCode()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var dbMock = GetCodes();
            mockRepo.Setup(x => x.Get()).Returns(dbMock);

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCodes = Assert.IsType<List<CodeDTO>>(okResult.Value);
            Assert.Equal(2, returnCodes.Count);
            Assert.Equal("Test One", returnCodes[0].Name);
            Assert.Equal("Test Two", returnCodes.FindLast(x => x.Name == "Test Two").Name);
        }
        #endregion

        #region Code_Get_Item
        [Fact]
        public async Task Get_ReturnsNotFound_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var returning = "invalid";
            var dbMock = GetCode(returning);
            mockRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(dbMock);

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsCodeDTO_GivenCode()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var dbMock = GetCode();
            mockRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(dbMock);

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCode = Assert.IsType<CodeDTO>(okResult.Value);
            Assert.Equal("Test One", returnCode.Name);
        }
        #endregion

        #region Code_Put_Item
        [Fact]
        public async Task Put_ReturnsBadRequest_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var returning = "invalid";
            var dbMock = GetCode(returning);
            mockRepo.Setup(x => x.Put(It.IsAny<int>(), It.IsAny<Code>()))
                .Returns(dbMock);

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Put(1, new CodeDTO(1, "Test One"));

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsCodeDTO_GivenCodeDTO()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var dbMock = GetCode();
            mockRepo.Setup(x => x.Put(It.IsAny<int>(), It.IsAny<Code>()))
                .Returns(dbMock).Verifiable();

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Put(1, new CodeDTO(1, "Test One"));

            // Assert
            mockRepo.VerifyAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCode = Assert.IsType<CodeDTO>(okResult.Value);
            Assert.Equal("Test One", returnCode.Name);
        }
        #endregion

        #region Code_Post_Item
        [Fact]
        public async Task Post_ReturnsBadRequest_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var returning = "invalid";
            var dbMock = GetCode(returning);
            mockRepo.Setup(x => x.Post(It.IsAny<Code>()))
                .Returns(dbMock);

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Post(new CodeDTO(1, "Test One"));

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCodeDTO_GivenCodeDTO()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var dbMock = GetCode();
            mockRepo.Setup(x => x.Post(It.IsAny<Code>()))
                .Returns(dbMock).Verifiable();

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Post(new CodeDTO(0, "Test One"));

            // Assert
            mockRepo.VerifyAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCode = Assert.IsType<CodeDTO>(okResult.Value);
            Assert.Equal("Test One", returnCode.Name);
        }
        #endregion

        #region Code_Delete_Item
        [Fact]
        public async Task Delete_ReturnsNotFound_GivenFalse()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var dbMock = GetBool(false);
            mockRepo.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(dbMock).Verifiable();

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Delete(1);

            // Assert
            mockRepo.VerifyAll();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotContent_GivenTrue()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var dbMock = GetBool(true);
            mockRepo.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(dbMock).Verifiable();

            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = await controller.Delete(1);

            // Assert
            mockRepo.VerifyAll();
            Assert.IsType<NoContentResult>(result);
        }
        #endregion

        #region Code_FromDTO
        [Fact]
        public void FromDTO_ReturnsCode_GivenCodeDTO()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = controller.FromDTO(new CodeDTO(new Code { CodeId = 1, Name = "Test One" }));

            // Assert
            Assert.IsType<Code>(result);
            Assert.Equal(1, result.CodeId);
            Assert.Equal("Test One", result.Name);
        }
        #endregion

        #region Code_ToDTO_Item
        [Fact]
        public void FromDTO_ReturnsCodeDTO_GivenCode()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = controller.ToDTO(new Code { CodeId = 1, Name = "Test One" });

            // Assert
            Assert.IsType<CodeDTO>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test One", result.Name);
        }
        #endregion

        #region Code_ToDTO_List
        [Fact]
        public void FromDTO_ReturnsListCodeDTO_GivenListCode()
        {
            // Arrange
            var mockRepo = new Mock<IService<Code>>();
            var controller = new CodesController(mockRepo.Object);

            // Act
            var result = controller.ToDTO(new List<Code>
            {
                new Code{ CodeId = 1, Name = "Test One" },
                new Code{ CodeId = 2, Name = "Test Two" }
            });

            // Assert
            Assert.IsType<List<CodeDTO>>(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Test One", result.First().Name);
            Assert.Equal("Test Two", result.Last().Name);
        }
        #endregion

        public async Task<IList<Code>> GetCodes(string operation = "")
        {
            if (operation.Equals("invalid")) return null;
            if (operation.Equals("empty")) return new List<Code>();

            return new List<Code>
            {
                new Code{ CodeId = 1, Name = "Test One" },
                new Code{ CodeId = 2, Name = "Test Two" }
            };
        }
        public async Task<Code> GetCode(string operation = "")
        {
            if (operation.Equals("invalid")) return null;
            if (operation.Equals("empty")) return new Code();

            return new Code { CodeId = 1, Name = "Test One" };
        }
        public async Task<bool> GetBool(bool value)
        {
            return value;
        }
    }
}
