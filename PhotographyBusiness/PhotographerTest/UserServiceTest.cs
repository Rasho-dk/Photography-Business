using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;

namespace PhotographerTest
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService userService;
        private User user;
        /// <summary>
        /// Ved Initialisere USerService har ma oprettet en instans af "UserService-klasse".
        /// Ved at oprette en instans af UserSerive-klasse, betyder at man kan bruge klassens metoder, der ikke statiske
        /// I UserService-Klasse har vi en konstrutør, der initializere en list af brugere fra Mockdata.
        /// Mock data bliver brugt til hver enkelt metod uafhængige af andre metoder. 
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            userService = new UserService();
        }
        /// <summary>
        /// GetAllUsers er en metod der retunere en liste over alle brugere i systemet.
        /// Vi starter med at kalder på metoden og gemmer resultatet i en variabel,der indholder disse værdier
        /// som er retuneret
        /// </summary>
        [TestMethod]
        public void Test_GetAllUser()
        {
            //Act : Tjekke om Listen indholder objekter
            List<User> result = userService.GetAllUsers();

            //Assert 
            Assert.IsTrue(result.Count()  > 0); 
        }
        /// Opret en User objekt 
        /// tjek om bruger er tilført til listen.
        /// </summary>
        [TestMethod]
        public void Test_CreateUser()
        {
            //Arrange
            var newUser = new User(1, "test@outlook.com", "test123", "test", "312312");

            //Act 
            userService.CreateUserAsync(newUser); // Tilføre en bruger til listen
            List<User> users = userService.GetAllUsers();

            //Assert       
            Assert.IsTrue(users.Contains(newUser));
        }

        /// <summary>
        /// tjek om en bruger kan slettes med en gyldig brugere id.       
        /// </summary>
        [TestMethod]
        public void Test_DeleteUserSuccessfully_ValidUserId()
        {
            //Arrange
            var newUser = new User(98, "test@outlook.com", "test123", "test", "312312");
            userService.CreateUserAsync(newUser);

            //Act
            var toBeDeleted = userService.DeleteUserAsync(98);
            List<User> users = userService.GetAllUsers();
            //Assert
            Assert.IsFalse(users.Contains(newUser));
        }

        /// <summary>
        /// slet en brugere med ugyldig id nummer
        /// </summary>
        /// <Assert>Tjek on den returne null hvis Bruger ID er ugyldig.</Assert>
        [TestMethod]
        public void Test_DeleteUser_With_InvaildUserId()
        {
            //Arrange
            int InvalidUSerId = 99;
            //Se om hvor mange bruger er i listen før den sletter.

            //Act
            var toBeDeleted = userService.DeleteUserAsync(InvalidUSerId);

            //Assert
            //Tjek om metoden returere null, hvis burger_ID passer ikke med de objekter i listen
            Assert.IsNull(toBeDeleted.Result); 
        }
        /// <summary>
        /// Henter all burgerer i Mock data ved at henvende sig i spesifike index i listen og tjekker om det passer
        /// --> med hinaden.
        /// </summary>
        [TestMethod]
        public void Test_GetUserByEmail()
        {
            //Arrange
            var actual_getUser = userService.GetAllUsers()[0];

            //Act
            var expected_User = userService.GetUserByEmailAsync(actual_getUser.Email);

            //Assert
            Assert.AreEqual(expected_User.Result.Email, actual_getUser.Email);
        }

        /// <summary>
        /// Prøver at klade på metoden med en ugyldig email og det skal returen null 
        /// </summary>
        [TestMethod]
        public void Test_GetUserByInvalidEamil()
        {
            //Arrange
            string InvaildEmail = "Invalid@hotmail.dk";

            //Act
            var resultOfInvalidEmail = userService.GetUserByEmailAsync(InvaildEmail);
            //Assert
            Assert.IsNull(resultOfInvalidEmail.Result);
        }
        [TestMethod]
        public void Test_GetUserByName()
        {
            //Arrange
            var getUserName_From_Index = userService.GetAllUsers()[0];

            //Act
            var resultOfGetUserByName = userService.GetUserByNameAsync(getUserName_From_Index.Name);

            //Assert
            Assert.AreEqual(resultOfGetUserByName.Result.Name, getUserName_From_Index.Name);
        }
        [TestMethod]
        public void Test_GetUserName_Invalid()
        {
            //Arrange
            var InvaildName = "Invalid Name";

            //Act
            var resultOfGetUserByName = userService.GetUserByNameAsync(InvaildName);

            //Assert
            Assert.IsNull(resultOfGetUserByName.Result);
        }
        [TestMethod]
        public void TestGetUsetById_ValidId()
        {
            //Arrange
            var GetUserByIndex = userService.GetAllUsers()[0];

            //Act
            var resultOFGetUserById = userService.GetUserByIdAsync(GetUserByIndex.UserId);

            //Asssert
            Assert.AreEqual(GetUserByIndex.UserId, resultOFGetUserById.Result.UserId);

        }
        [TestMethod]
        public void TestGetUsetById_InValidId()
        {
            //Arrange
            var Invalid_UserId = 22;

            //Act
            var resultOFGetUserById = userService.GetUserByIdAsync(Invalid_UserId);

            //Asssert
            Assert.IsNull(resultOFGetUserById.Result);
        }
        [TestMethod]
        public void TestUpdateUserInfo()
        {
            //Arrange : Klader på metod GetAllUsers Og på en specifik index og opdatere inholdet.
            var actual = userService.GetAllUsers()[0];
            actual.Name = "Foo";
            actual.PhoneNumber = "222312123";
            //Act
            userService.UpdateUserAsync(actual); // Her hvor man opdatte brugeres info.

            //ved hjælp af den metod indsater man parameterene som kommer fra GetAllUsers() metod og vælger man Name.
            //for at hente alle oplysninger når navnet passer med hinaden. 
            var expected = userService.GetUserByNameAsync(actual.Name);
            //Assert: Her tjekker man om
            Assert.AreEqual(expected.Result.Name, actual.Name);
            Assert.AreEqual(expected.Result.PhoneNumber, actual.PhoneNumber);
        }
        [TestMethod]
        public void TestUpdateUserIfo_UserIsNull()
        {
            //Arrange 
            User user = null;
            // Act
           var result =  userService.UpdateUserAsync(user);

            //Assert
            Assert.IsNull(result.Result);
        }       
    }
}
