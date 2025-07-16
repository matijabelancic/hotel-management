using Api.Entities;
using Api.Repositories;
using Api.Utilities.Exceptions;

namespace Tests
{
    public class HotelRepositoryTests
    {
        private readonly HotelRepository _repository;

        public HotelRepositoryTests()
        {
            _repository = new HotelRepository();
        }

        [Fact]
        public void GetAll_ShouldReturnAllHotels()
        {
         
            var hotels = _repository.GetAll();
            
            Assert.NotNull(hotels);
            Assert.Equal(10, hotels.Count());
        }

        [Fact]
        public void GetById_ShouldReturnHotel_WhenIdExists()
        {
         
            var existingHotel = _repository.GetAll().First();
            
            var result = _repository.GetById(existingHotel.Id);
            
            Assert.NotNull(result);
            Assert.Equal(existingHotel.Id, result.Id);
        }

        [Fact]
        public void GetById_ShouldThrow_WhenHotelDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            
            Assert.Throws<HotelNotFoundException>(() => _repository.GetById(invalidId));
        }

        [Fact]
        public void Create_ShouldAddNewHotel()
        {
            var newHotel = new Hotel
            {
                Id = Guid.NewGuid(),
                Name = "Test Hotel",
                Price = 100,
                Latitude = 44.0,
                Longitude = 15.0
            };

         
            _repository.Create(newHotel);
            var result = _repository.GetById(newHotel.Id);
            
            Assert.NotNull(result);
            Assert.Equal("Test Hotel", result.Name);
            Assert.Equal(100, result.Price);
        }

        [Fact]
        public void Update_ShouldModifyExistingHotel()
        {
            var existing = _repository.GetAll().First();
            var updatedHotel = new Hotel
            {
                Id = existing.Id,
                Name = "Updated Hotel",
                Price = 200,
                Latitude = existing.Latitude,
                Longitude = existing.Longitude
            };
            
            _repository.Update(updatedHotel);
            var result = _repository.GetById(existing.Id);
            
            Assert.Equal("Updated Hotel", result.Name);
            Assert.Equal(200, result.Price);
        }

        [Fact]
        public void Update_ShouldThrow_WhenHotelDoesNotExist()
        {
            var nonExisting = new Hotel
            {
                Id = Guid.NewGuid(),
                Name = "Nonexistent Hotel",
                Price = 50,
                Latitude = 0,
                Longitude = 0
            };
            
            Assert.Throws<HotelNotFoundException>(() => _repository.Update(nonExisting));
        }

        [Fact]
        public void Delete_ShouldRemoveHotel()
        {
            var hotel = _repository.GetAll().First();
            var hotelId = hotel.Id;
            
            _repository.Delete(hotelId);
            
            Assert.Throws<HotelNotFoundException>(() => _repository.GetById(hotelId));
        }

        [Fact]
        public void Delete_ShouldThrow_WhenHotelDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            
            Assert.Throws<HotelNotFoundException>(() => _repository.Delete(nonExistingId));
        }
    }
}
