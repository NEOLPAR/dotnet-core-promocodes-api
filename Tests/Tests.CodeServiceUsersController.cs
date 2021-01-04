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
    public class CodeServiceUsersControllerTests
    {
        #region CodeServiceUser_Get_List
        [Fact]
        public async Task Get_ReturnsBadRequest_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var returning = "invalid";
            var dbMock = GetList(returning);
            mockRepo.Setup(x => x.Get()).Returns(dbMock);

            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsEmptyListCodeServiceUserDTO_GivenEmptyList()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var returning = "empty";
            var dbMock = GetList(returning);
            mockRepo.Setup(x => x.Get()).Returns(dbMock);

            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnList = Assert.IsType<List<CodeServiceUserDTO>>(okResult.Value);
            Assert.Empty(returnList);
        }

        [Fact]
        public async Task Get_ReturnsListCodeServiceUserDTO_GivenListCodeServiceUser()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var dbMock = GetList();
            mockRepo.Setup(x => x.Get()).Returns(dbMock);

            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            mockRepo.VerifyAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnList = Assert.IsType<List<CodeServiceUserDTO>>(okResult.Value);
            Assert.Equal(3, returnList.Count);
            Assert.Equal(1, returnList.First().CodeId);
            Assert.Equal(2, returnList[1].CodeId);
            Assert.Equal(2, returnList.Last().CodeId);
        }
        #endregion

        #region CodeServiceUser_Get_Item
        [Fact]
        public async Task Get_ReturnsNotFound_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var returning = "invalid";
            var dbMock = GetItem(returning);
            mockRepo.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>())).Returns(dbMock);

            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = await controller.Get(1, 2);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsCodeServiceUserDTO_GivenCodeServiceUser()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var dbMock = GetItem();
            mockRepo.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>())).Returns(dbMock);

            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = await controller.Get(1, 2);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnItem = Assert.IsType<CodeServiceUserDTO>(okResult.Value);
            Assert.Equal(1, returnItem.CodeId);
            Assert.Equal(2, returnItem.ServiceId);
        }
        #endregion

        #region CodeServiceUser_Post_Item
        [Fact]
        public async Task Post_ReturnsBadRequest_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var returning = "invalid";
            var dbMock = GetItem(returning);
            mockRepo.Setup(x => x.Post(It.IsAny<CodeServiceUser>()))
                .Returns(dbMock);

            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = await controller.Post(
                new CodeServiceUserDTO(1, "Code One", 2, "Service Two", "moqUser", false));

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsCodeServiceUserDTO_GivenCodeServiceUserDTO()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var dbMock = GetItem();
            mockRepo.Setup(x => x.Post(It.IsAny<CodeServiceUser>()))
                .Returns(dbMock).Verifiable();

            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = await controller.Post(
                new CodeServiceUserDTO(1, "Code One", 2, "Service Two", "moqUser", false));

            // Assert
            mockRepo.VerifyAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnItem = Assert.IsType<CodeServiceUserDTO>(okResult.Value);
            Assert.Equal(1, returnItem.CodeId);
            Assert.Equal("Code One", returnItem.CodeName);
            Assert.Equal(2, returnItem.ServiceId);
            Assert.Equal("Service Two", returnItem.ServiceName);
            Assert.Equal("moqUser", returnItem.UserName);
            Assert.False(returnItem.Enabled);
        }
        #endregion
        
        #region CodeServiceUser_Delete_Item
        [Fact]
        public async Task Delete_ReturnsNotFound_GivenFalse()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var dbMock = GetBool(false);
            mockRepo.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(dbMock).Verifiable();

            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = await controller.Delete(1, 1);

            // Assert
            mockRepo.VerifyAll();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotContent_GivenTrue()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var dbMock = GetBool(true);
            mockRepo.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(dbMock).Verifiable();

            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = await controller.Delete(1, 1);

            // Assert
            mockRepo.VerifyAll();
            Assert.IsType<NoContentResult>(result);
        }
        #endregion
        
        #region CodeServiceUser_FromDTO
        [Fact]
        public void FromDTO_ReturnsCodeServiceUser_GivenCodeServiceUserDTO()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = controller.FromDTO(new CodeServiceUserDTO(GetItemSync()));

            // Assert
            Assert.IsType<CodeServiceUser>(result);
            Assert.Equal(1, result.CodeId);
            Assert.Equal(2, result.ServiceId);
            Assert.Equal("moqUser", result.UserName);
            Assert.False(result.Enabled);
        }
        #endregion
        
        #region CodeServiceUser_ToDTO_Item
        [Fact]
        public void FromDTO_ReturnsCodeServiceUserDTO_GivenCodeServiceUser()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = controller.ToDTO(GetItemSync());

            // Assert
            Assert.IsType<CodeServiceUserDTO>(result);
            Assert.Equal(1, result.CodeId);
            Assert.Equal("Code One", result.CodeName);
            Assert.Equal(2, result.ServiceId);
            Assert.Equal("Service Two", result.ServiceName);
            Assert.Equal("moqUser", result.UserName);
            Assert.False(result.Enabled);
        }
        #endregion
        
        #region CodeServiceUser_ToDTO_List
        [Fact]
        public void FromDTO_ReturnsListCodeServiceUserDTO_GivenListCodeServiceUser()
        {
            // Arrange
            var mockRepo = new Mock<ICodeServiceUserService>();
            var controller = new CodeServiceUsersController(mockRepo.Object);

            // Act
            var result = controller.ToDTO(GetListSync());

            // Assert
            Assert.IsType<List<CodeServiceUserDTO>>(result);
            Assert.Equal(3, result.Count);

            Assert.Equal(1, result.First().CodeId);
            Assert.Equal("Code One", result.First().CodeName);
            Assert.Equal(2, result.First().ServiceId);
            Assert.Equal("Service Two", result.First().ServiceName);
            Assert.Equal("moqUser", result.First().UserName);
            Assert.False(result.First().Enabled);


            Assert.Equal(2, result[1].CodeId);
            Assert.Equal("Code Two", result[1].CodeName);
            Assert.Equal(1, result[1].ServiceId);
            Assert.Equal("Service One", result[1].ServiceName);
            Assert.Equal("moqUser", result[1].UserName);
            Assert.True(result[1].Enabled);


            Assert.Equal(2, result.Last().CodeId);
            Assert.Equal("Code Two", result.Last().CodeName);
            Assert.Equal(2, result.Last().ServiceId);
            Assert.Equal("Service Two", result.Last().ServiceName);
            Assert.Equal("moqUser2", result.Last().UserName);
            Assert.True(result.Last().Enabled);
        }
        #endregion

        public async Task<IList<CodeServiceUser>> GetList(string operation = "")
        {
            return GetListSync(operation);
        }
        public static IList<CodeServiceUser> GetListSync(string operation = "")
        {
            if (operation.Equals("invalid")) return null;
            if (operation.Equals("empty")) return new List<CodeServiceUser>();

            var newList = new List<CodeServiceUser>
                {
                    new CodeServiceUser{
                        CodeId = 1,
                        ServiceId = 2,
                        UserName = "moqUser",
                        Enabled = false
                    },
                    new CodeServiceUser{
                        CodeId = 2,
                        ServiceId = 1,
                        UserName = "moqUser",
                        Enabled = true
                    },
                    new CodeServiceUser{
                        CodeId = 2,
                        ServiceId = 2,
                        UserName = "moqUser2",
                        Enabled = true
                    },
                };

            newList[0].Code = new Code { CodeId = 1, Name = "Code One" };
            newList[1].Code = new Code { CodeId = 2, Name = "Code Two" };
            newList[2].Code = new Code { CodeId = 2, Name = "Code Two" };
            newList[0].Service = new Service { ServiceId = 2, Name = "Service Two" };
            newList[1].Service = new Service { ServiceId = 1, Name = "Service One" };
            newList[2].Service = new Service { ServiceId = 2, Name = "Service Two" };

            return newList;
        }
        public async Task<CodeServiceUser> GetItem(string operation = "")
        {
            return GetItemSync(operation);
        }
        public static CodeServiceUser GetItemSync(string operation = "")
        {
            if (operation.Equals("invalid")) return null;
            if (operation.Equals("empty")) return new CodeServiceUser();

            var newItem = new CodeServiceUser
            {
                CodeId = 1,
                ServiceId = 2,
                UserName = "moqUser",
                Enabled = false
            };

            newItem.Code = new Code { CodeId = 1, Name = "Code One" };
            newItem.Service = new Service { ServiceId = 2, Name = "Service Two" };

            return newItem;
        }
        public async Task<bool> GetBool(bool value)
        {
            return value;
        }
    }
}
