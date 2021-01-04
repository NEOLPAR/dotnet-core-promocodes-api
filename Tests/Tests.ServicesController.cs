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
using System.Security.Principal;
using System.Security.Claims;

namespace Tests
{
    public class ServicesControllerTests
    {
        #region Service_Get_List
        [Fact]
        public async Task Get_ReturnsBadRequest_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var returning = "invalid";
            var dbMock = GetServices(null, returning);
            mockRepo.Setup(x => x.Get()).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsEmptyListServiceDTO_GivenEmptyList()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var returning = "empty";
            var dbMock = GetServices(null, returning);
            mockRepo.Setup(x => x.Get()).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnServices = Assert.IsType<List<ServiceDTO>>(okResult.Value);
            Assert.Empty(returnServices);
        }

        [Fact]
        public async Task Get_ReturnsListServiceDTO_GivenListService()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetServices(CodeServiceUsersControllerTests.GetListSync());
            mockRepo.Setup(x => x.Get()).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnServices = Assert.IsType<List<ServiceDTO>>(okResult.Value);
            Assert.Equal(3, returnServices.Count);
            Assert.Equal("Test One", returnServices.First().Name);
            Assert.Equal("Test Three", returnServices[1].Name);
            Assert.Equal("Test Two", returnServices.Last().Name);
        }
        #endregion

        #region Service_Get_Item
        [Fact]
        public async Task Get_ReturnsNotFound_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var returning = "invalid";
            var dbMock = GetService(null, returning);
            mockRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsServiceDTO_GivenService()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetService(CodeServiceUsersControllerTests.GetListSync());
            mockRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnService = Assert.IsType<ServiceDTO>(okResult.Value);
            Assert.Equal("Test One", returnService.Name);
        }
        #endregion

        #region Service_Put_Item
        [Fact]
        public async Task Put_ReturnsBadRequest_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var returning = "invalid";
            var dbMock = GetService(null, returning);
            mockRepo.Setup(x => x.Put(It.IsAny<int>(), It.IsAny<Service>()))
                .Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Put(1, new ServiceDTO(1, "Test One", "Description One"));

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsServiceDTO_GivenServiceDTO()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetService(CodeServiceUsersControllerTests.GetListSync());
            mockRepo.Setup(x => x.Put(It.IsAny<int>(), It.IsAny<Service>()))
                .Returns(dbMock).Verifiable();

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Put(1, new ServiceDTO(1, "Test One", "Description One"));

            // Assert
            mockRepo.VerifyAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnService = Assert.IsType<ServiceDTO>(okResult.Value);
            Assert.Equal("Test One", returnService.Name);
        }
        #endregion

        #region Service_Post_Item
        [Fact]
        public async Task Post_ReturnsBadRequest_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var returning = "invalid";
            var dbMock = GetService(null, returning);
            mockRepo.Setup(x => x.Post(It.IsAny<Service>()))
                .Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Post(new ServiceDTO(1, "Test One", "Description One"));

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsServiceDTO_GivenServiceDTO()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetService(CodeServiceUsersControllerTests.GetListSync());
            mockRepo.Setup(x => x.Post(It.IsAny<Service>()))
                .Returns(dbMock).Verifiable();

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Post(new ServiceDTO(0, "Test One", "Description One"));

            // Assert
            mockRepo.VerifyAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnService = Assert.IsType<ServiceDTO>(okResult.Value);
            Assert.Equal("Test One", returnService.Name);
        }
        #endregion

        #region Service_Delete_Item
        [Fact]
        public async Task Delete_ReturnsNotFound_GivenFalse()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetBool(false);
            mockRepo.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(dbMock).Verifiable();

            var controller = new ServicesController(mockRepo.Object);

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
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetBool(true);
            mockRepo.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(dbMock).Verifiable();

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.Delete(1);

            // Assert
            mockRepo.VerifyAll();
            Assert.IsType<NoContentResult>(result);
        }
        #endregion

        #region Service_FromDTO
        [Fact]
        public void FromDTO_ReturnsService_GivenServiceDTO()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = controller.FromDTO(
                new ServiceDTO(GetServiceSync(CodeServiceUsersControllerTests.GetListSync()), "moqUser"));

            // Assert
            Assert.IsType<Service>(result);
            Assert.Equal(1, result.ServiceId);
            Assert.Equal("Test One", result.Name);
        }
        #endregion

        #region Service_ToDTO_Item
        [Fact]
        public void FromDTO_ReturnsServiceDTO_GivenService()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = controller.ToDTO(GetServiceSync(CodeServiceUsersControllerTests.GetListSync()));

            // Assert
            Assert.IsType<ServiceDTO>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test One", result.Name);
        }
        #endregion

        #region Service_ToDTO_List
        [Fact]
        public void FromDTO_ReturnsListServiceDTO_GivenListService()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            mockRepo.Setup(x => x.CurrentUserName()).Returns("moqUser");

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = controller.ToDTO(GetServicesSync(CodeServiceUsersControllerTests.GetListSync()));

            // Assert
            Assert.IsType<List<ServiceDTO>>(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Test One", result.First().Name);
            Assert.Equal("Test Three", result[1].Name);
            Assert.Equal("Test Two", result.Last().Name);
        }
        #endregion

        #region Service_GetByName
        [Fact]
        public async Task GetByName_ReturnsNotContent_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var returning = "invalid";
            var dbMock = GetService(null, returning);
            mockRepo.Setup(x => x.GetByName(It.IsAny<string>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.GetByName("Service Name");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetByName_ReturnsServiceDTO_GivenService()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetService(CodeServiceUsersControllerTests.GetListSync());
            mockRepo.Setup(x => x.GetByName(It.IsAny<string>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.GetByName("Service Name");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnService = Assert.IsType<ServiceDTO>(okResult.Value);
            Assert.Equal("Test One", returnService.Name);
        }
        #endregion

        #region Service_GetInfiniteScroll
        [Fact]
        public async Task GetInfiniteScroll_ReturnsNotContent_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var returning = "invalid";
            var dbMock = GetServices(null, returning);
            mockRepo.Setup(x => x.GetInfiniteScroll(It.IsAny<int>(), It.IsAny<int>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.GetInfiniteScroll(1, 3);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetInfiniteScroll_ReturnsServiceDTO_GivenService()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetServices(CodeServiceUsersControllerTests.GetListSync());
            mockRepo.Setup(x => x.GetInfiniteScroll(It.IsAny<int>(), It.IsAny<int>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.GetInfiniteScroll(1, 3);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnService = Assert.IsType<List<ServiceDTO>>(okResult.Value);
            Assert.Equal(3, returnService.Count);
            Assert.Equal("Test One", returnService.First().Name);
        }
        #endregion

        #region Service_FilterByNameInfiniteScroll
        [Fact]
        public async Task FilterByNameInfiniteScroll_ReturnsNotContent_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var returning = "invalid";
            var dbMock = GetServices(null, returning);
            mockRepo.Setup(x => x.FilterByNameInfiniteScroll(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.FilterByNameInfiniteScroll("service", 1, 3);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task FilterByNameInfiniteScroll_ReturnsServiceDTO_GivenService()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetServices(CodeServiceUsersControllerTests.GetListSync());
            mockRepo.Setup(x => x.FilterByNameInfiniteScroll(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.FilterByNameInfiniteScroll("Test", 1, 3);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnService = Assert.IsType<List<ServiceDTO>>(okResult.Value);
            Assert.Equal(3, returnService.Count);
            Assert.Equal("Test One", returnService.First().Name);
        }
        #endregion

        #region Service_FilterByName
        [Fact]
        public async Task FilterByName_ReturnsNotContent_GivenNull()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var returning = "invalid";
            var dbMock = GetServices(null, returning);
            mockRepo.Setup(x => x.FilterByName(It.IsAny<string>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.FilterByName("Test");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task FilterByName_ReturnsServiceDTO_GivenService()
        {
            // Arrange
            var mockRepo = new Mock<IServiceService<Service>>();
            var dbMock = GetServices(CodeServiceUsersControllerTests.GetListSync());
            mockRepo.Setup(x => x.FilterByName(It.IsAny<string>())).Returns(dbMock);

            var controller = new ServicesController(mockRepo.Object);

            // Act
            var result = await controller.FilterByName("Test");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnService = Assert.IsType<List<ServiceDTO>>(okResult.Value);
            Assert.Equal(3, returnService.Count);
            Assert.Equal("Test One", returnService.First().Name);
        }
        #endregion

        public async Task<IList<Service>> GetServices(IList<CodeServiceUser> codeUserService,
            string operation = "") => GetServicesSync(codeUserService, operation);

        public IList<Service> GetServicesSync(IList<CodeServiceUser> codeUserService, string operation = "")
        {
            if (operation.Equals("invalid")) return null;
            if (operation.Equals("empty")) return new List<Service>();

            return new List<Service>
            {
                new Service{
                    ServiceId = 1,
                    Name = "Test One",
                    Description = "Description One",
                    CodesServicesUsers = codeUserService
                },
                new Service{
                    ServiceId = 3,
                    Name = "Test Three",
                    Description = "Description Three",
                    CodesServicesUsers = codeUserService
                },
                new Service{ 
                    ServiceId = 2, 
                    Name = "Test Two", 
                    Description = "Description Two", 
                    CodesServicesUsers = codeUserService 
                }
            };
        }
        public async Task<Service> GetService(IList<CodeServiceUser> codeUserService,
            string operation = "") => GetServiceSync(codeUserService, operation);
        public Service GetServiceSync(IList<CodeServiceUser> codeUserService, string operation = "")
        {
            if (operation.Equals("invalid")) return null;
            if (operation.Equals("empty")) return new Service();

            return new Service
                {
                    ServiceId = 1,
                    Name = "Test One",
                    Description = "Description One",
                    CodesServicesUsers = codeUserService
                };
        }
        public async Task<bool> GetBool(bool value)
        {
            return value;
        }
    }
}
