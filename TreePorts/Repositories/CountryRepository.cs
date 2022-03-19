using System.Linq.Expressions;

namespace TreePorts.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private TreePortsDBContext _context;

        public CountryRepository(TreePortsDBContext context)
        {
            _context = context;
        }

        public async Task<CountryPrice?> DeleteCountryPriceAsync(long id, CancellationToken cancellationToken)
        {
            var countryPrice = await _context.CountryPrices.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);
            if (countryPrice == null) return null;

            countryPrice.IsDeleted = true;
            countryPrice.IsCurrent = false;
            countryPrice.ModificationDate =DateTime.Now;
            _context.Entry<CountryPrice>(countryPrice).State = EntityState.Modified;
            return countryPrice;
        }

        public async Task<List<CountryOrderPrice>> GetCountriesOrderPricesAsync(CancellationToken cancellationToken)
        {
            return await _context.CountryOrderPrices.ToListAsync(cancellationToken);
        }

        public async Task<CountryOrderPrice?> GetCountryOrderPriceByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CountryOrderPrices.FirstOrDefaultAsync(cop => cop.Id == id,cancellationToken);
        }

        public async Task<List<CountryOrderPrice>> GetCountriesOrderPricesByAsync(Expression<Func<CountryOrderPrice, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.CountryOrderPrices.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<CountryOrderPrice> InsertCountryOrderPriceAsync(CountryOrderPrice countryOrderPrice, CancellationToken cancellationToken)
        {
            countryOrderPrice.CreationDate = DateTime.Now;
            var insertResult = await _context.CountryOrderPrices.AddAsync(countryOrderPrice,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<CountryOrderPrice?> DeleteCountryOrderPriceAsync(long id, CancellationToken cancellationToken)
        {
            var oldCountryOrderPrice =
                await _context.CountryOrderPrices.FirstOrDefaultAsync(cop => cop.Id == id, cancellationToken);
            if (oldCountryOrderPrice == null) return null;

            _context.CountryOrderPrices.Remove(oldCountryOrderPrice);
            return oldCountryOrderPrice;
        }

        public async Task<IEnumerable<CountryProductPrice>?> DeleteCountryProductPriceByCountryIdAsync(long id, CancellationToken cancellationToken)
        {
            var countryProductsPrices = _context.CountryProductPrices.Where(c => c.CountryId == id); //.ToListAsync(cancellationToken);
            if (countryProductsPrices == null) return null;

             await countryProductsPrices.ForEachAsync( c => c.IsDeleted = true,cancellationToken);
            _context.Entry<IEnumerable<CountryProductPrice>>(countryProductsPrices).State = EntityState.Modified;
            return countryProductsPrices;
        }

        public async Task<CountryProductPrice?> DeleteCountryProductPriceAsync(long id, CancellationToken cancellationToken)
        {
            var countryProductPrice = await _context.CountryProductPrices.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);
            if (countryProductPrice == null) return null;

            countryProductPrice.IsDeleted = true;
            _context.Entry<CountryProductPrice>(countryProductPrice).State = EntityState.Modified;
            return countryProductPrice;
        }

        public async Task<IEnumerable<CountryProductPrice>?> DeleteCountryProductPriceByProductIdAsync(long id, CancellationToken cancellationToken)
        {
            var ProductcountriesPrices = _context.CountryProductPrices.Where(c => c.ProductTypeId == id);//.ToListAsync(cancellationToken);
            if (ProductcountriesPrices == null) return null;

            await ProductcountriesPrices.ForEachAsync(c => c.IsDeleted = true,cancellationToken);
            _context.Entry<IEnumerable<CountryProductPrice>>(ProductcountriesPrices).State = EntityState.Modified;
            return ProductcountriesPrices;
        }


        //public async Task<List<CityPrice>> GetAllCitiesPrices()
        //{
        //    return await _context.CityPrices.Where(cp => cp.IsDeleted == false).ToListAsync(cancellationToken);
        //}

        public async Task<object> GetCitiesPricesAsync(CancellationToken cancellationToken)
        {

            var data = from cityPrice in _context.CityPrices.Where(cp => cp.IsDeleted == false).AsNoTracking()
                       join city in _context.Cities.AsNoTracking() on cityPrice.CityId equals city.Id
                       join country in _context.Countries.AsNoTracking() on city.CountryId equals country.Id
                       select new { Country = country, City = city ,CityPrice = cityPrice };
            return await data.ToListAsync(cancellationToken);
        }



        public async Task<object?> GetCityPriceByIdAsync(long id, CancellationToken cancellationToken)
        {
            var data = from cityPrice in _context.CityPrices.Where(cp => cp.Id == id).AsNoTracking()
                       join city in _context.Cities.AsNoTracking() on cityPrice.CityId equals city.Id
                       join country in _context.Countries.AsNoTracking() on city.CountryId equals country.Id
                       select new { Country = country, City = city, CityPrice = cityPrice };
            return await data.FirstOrDefaultAsync(cancellationToken);
        }

        //public async Task<CityPrice> GetCityPriceById(long id)
        //{
        //    return await _context.CityPrices.Where(cp => cp.Id == id).FirstOrDefaultAsync(cancellationToken);
        //}

        public async Task<List<CityPrice>> GetCitiesPricesByAsync(Expression<Func<CityPrice, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.CityPrices.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<CityPrice> InsertCityPriceAsync(CityPrice cityPrice, CancellationToken cancellationToken)
        {
            var oldCityPrice = await _context.CityPrices.FirstOrDefaultAsync(cp => cp.CityId == cityPrice.CityId && cp.IsCurrent == true,cancellationToken);
            if (oldCityPrice != null)
            {
                oldCityPrice.IsCurrent = false;
                oldCityPrice.ModificationDate=DateTime.Now;
                _context.Entry<CityPrice>(oldCityPrice).State = EntityState.Modified;
            }

            cityPrice.IsCurrent = true;
            cityPrice.CreationDate = DateTime.Now;
            var insertResult = await _context.CityPrices.AddAsync(cityPrice,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<CityPrice?> DeleteCityPriceAsync(long id, CancellationToken cancellationToken)
        {
            var oldCityPrice = await _context.CityPrices.FirstOrDefaultAsync(cp => cp.Id == id,  cancellationToken);
            if (oldCityPrice == null) return null;

            oldCityPrice.IsDeleted = true;
            _context.Entry<CityPrice>(oldCityPrice).State = EntityState.Modified;
            return oldCityPrice;
        }

        public async Task<List<CityOrderPrice>> GetCitiesOrderPricesAsync(CancellationToken cancellationToken)
        {
            return await _context.CityOrderPrices.ToListAsync(cancellationToken);
        }

        public async Task<CityOrderPrice?> GetCityOrderPriceByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CityOrderPrices.FirstOrDefaultAsync(cop => cop.Id == id,cancellationToken);
        }

        public async Task<List<CityOrderPrice>> GetCitiesOrderPricesByAsync(Expression<Func<CityOrderPrice, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.CityOrderPrices.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<CityOrderPrice> InsertCityOrderPriceAsync(CityOrderPrice cityOrderPrice, CancellationToken cancellationToken)
        {
            cityOrderPrice.CreationDate = DateTime.Now;
            var insertResult = await _context.CityOrderPrices.AddAsync(cityOrderPrice,cancellationToken);
            return insertResult.Entity;
        }

        public async Task<CityOrderPrice?> DeleteCityOrderPriceAsync(long id, CancellationToken cancellationToken)
        {
            var oldCityOrderPrice = await _context.CityOrderPrices.FirstOrDefaultAsync(cop => cop.Id == id,cancellationToken);
            if (oldCityOrderPrice == null) return null;
            
            _context.CityOrderPrices.Remove(oldCityOrderPrice);
            return oldCityOrderPrice;
        }

        



        public async Task<IEnumerable<CountryResponse>> GetCountriesAsync(CancellationToken cancellationToken)
        {
            //return   _context.Countries.AsAsyncEnumerable().ConfigureAwait(false);
            /*await foreach (Country country in _context.Countries.AsAsyncEnumerable())
            {
                yield return country;
            }*/

            return await _context.Countries.Select( country => new CountryResponse
            {
                Id = country.Id,
                ArabicName = country.ArabicName,
                Code = country.Code,
                Name = country.Name,
                Iso = country.Iso,
                CurrencyIso = country.CurrencyIso,
                CurrencyArabicName = country.CurrencyArabicName,
                CurrencyName = country.CurrencyName,
                ModificationDate = country.ModificationDate,
                CreationDate = country.CreationDate
            }).ToListAsync(cancellationToken);
        }

        // public async Task<List<Country>> GetAll()
        // {
        //     return await _context.Countries.ToListAsync(cancellationToken);
        // }

        //public async Task<List<CountryPrice>> GetAllCountriesPrices()
        //{
        //    return await _context.CountryPrices.Include(cp => cp.Country).Where(cp => cp.IsDeleted == false).ToListAsync(cancellationToken);
        //}



        public async Task<object> GetCountriesPricesAsync(CancellationToken cancellationToken)
        {
            var data = from countryPrice in _context.CountryPrices.Where(cp => cp.IsDeleted == false).AsNoTracking()
                       join country in _context.Countries.AsNoTracking() on countryPrice.CountryId equals country.Id
                       select new { Country = country, CountryPrice = countryPrice };
            return await data.ToListAsync(cancellationToken);
        }



        public async Task<List<CountryProductPrice>> GetCountriesProductsPricesAsync(CancellationToken cancellationToken)
        {
            return await _context.CountryProductPrices.Where(cpp => cpp.IsDeleted == false).ToListAsync(cancellationToken);
        }

        public async Task<object?> GetCountryAllDataAsync(long id, CancellationToken cancellationToken)
        {


            return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);

            /* var data = from country in _context.Countries
                        join countryPrice in _context.CountryPrices
                        on country.Id equals countryPrice.CountryId into prices
                        from left in prices.DefaultIfEmpty()
                            *//* join countryProductsPrices in _context.CountryProductPrices
                             on country.Id equals countryProductsPrices.CountryId  into productsPrices*//*
                        where country.Id == id
                        select new { Country = country, CountryPrices = left }; //, CountryProductsPrices = productsPrices };

             return await data.ToListAsync(cancellationToken);*/
        }

        public async Task<List<Country>> GetCountriesByAsync(Expression<Func<Country, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Countries.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<CountryResponse?> GetCountryByIdAsync(long id, CancellationToken cancellationToken)
        {
            /*return await _context.Countries.Where(c => c.Id == ID).FirstOrDefaultAsync(cancellationToken);*/
            return await _context.Countries.Select(country => new CountryResponse
            {
                Id = country.Id,
                ArabicName = country.ArabicName,
                Code = country.Code,
                Name = country.Name,
                Iso = country.Iso,
                CurrencyIso = country.CurrencyIso,
                CurrencyArabicName = country.CurrencyArabicName,
                CurrencyName = country.CurrencyName,
                ModificationDate = country.ModificationDate,
                CreationDate = country.CreationDate,
                Cities = country.Cities.Select( city => new CityResponse {
                    Id = city.Id,
                    Name = city.Name,
                    ArabicName= city.ArabicName,
                    CountryId = city.CountryId,
                    CreationDate= city.CreationDate,
                    ModificationDate = city.ModificationDate
                }).ToList()
            }).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<CountryPrice>> GetCountriesPricesByAsync(Expression<Func<CountryPrice, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.CountryPrices.Where(predicate).ToListAsync(cancellationToken);
        }


        //public async Task<CountryPrice> GetCountryPriceByID(long ID)
        //{
        //    return await _context.CountryPrices.Where(cp => cp.Id == ID).FirstOrDefaultAsync(cancellationToken);
        //}

        public async Task<object?> GetCountryPriceByIdAsync(long id, CancellationToken cancellationToken)
        {
            var data = from countryPrice in _context.CountryPrices.Where(cp => cp.Id == id).AsNoTracking()
                       join country in _context.Countries.AsNoTracking() on countryPrice.CountryId equals country.Id
                       select new { Country = country, CountryPrice = countryPrice };
            return await data.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<CountryProductPrice>> GetCountriesProductsPricesByAsync(Expression<Func<CountryProductPrice, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.CountryProductPrices.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<CountryProductPrice?> GetCountryProductPriceByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.CountryProductPrices.FirstOrDefaultAsync(cpp => cpp.Id == id,cancellationToken);
        }

        public async Task<Country> InsertCountryAsync(Country country, CancellationToken cancellationToken)
        {
            country.CreationDate = DateTime.Now;
            var result =  await _context.Countries.AddAsync(country, cancellationToken);
            return result.Entity;
        }


        public async Task<Country?> UpdateCountryAsync(Country country, CancellationToken cancellationToken) {
            var oldCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == country.Id,cancellationToken);
            if (oldCountry == null) return null;

            oldCountry.Name = country.Name;
            oldCountry.ArabicName = country.ArabicName;
            oldCountry.Code = country.Code;
            oldCountry.CurrencyArabicName = country.CurrencyArabicName;
            oldCountry.CurrencyIso = country.CurrencyIso;
            oldCountry.CurrencyName = country.CurrencyName;
            oldCountry.Iso = country.Iso;
            oldCountry.ModificationDate = DateTime.Now;
            _context.Entry<Country>(oldCountry).State = EntityState.Modified;
            return oldCountry;

        }

        public async Task<List<Country>> InsertCountriesAsync(List<Country> countries, CancellationToken cancellationToken)
        {
            countries.ForEach( c => c.CreationDate = DateTime.Now);
            await _context.Countries.AddRangeAsync(countries,cancellationToken);
            return countries;
        }

        public async Task<List<CountryPrice>> InsertCountriesPricesAsync(List<CountryPrice> countriesPrices, CancellationToken cancellationToken)
        {
            await _context.CountryPrices.AddRangeAsync(countriesPrices,cancellationToken);
            return countriesPrices;
        }

        public async Task<List<CountryProductPrice>> InsertCountriesProductsPricesAsync(List<CountryProductPrice> countriesProductsPrices, CancellationToken cancellationToken)
        {
            await _context.CountryProductPrices.AddRangeAsync(countriesProductsPrices,cancellationToken);
            return countriesProductsPrices;
        }

        // public async Task<CountryPrice> InsertCountryPrice(CountryPrice countryPrice)
        // {
        //     var oldCountryPrice = await _context.CountryPrices.Where(cp => cp.CountryId == countryPrice.CountryId).FirstOrDefaultAsync(cancellationToken);
        //     if (oldCountryPrice != null)
        //     {
        //         await InsertCountryPriceHistory(oldCountryPrice);
        //         var result = UpdateCountryPrice(oldCountryPrice,countryPrice);
        //         return result;
        //     }
        //     else {
        //         var result = await _context.CountryPrices.AddAsync(countryPrice);
        //         return result.Entity;
        //     }
        // }
        
        
        public async Task<CountryPrice> InsertCountryPriceAsync(CountryPrice countryPrice, CancellationToken cancellationToken)
        {
            var oldCountryPrice = await _context.CountryPrices.FirstOrDefaultAsync(cp => cp.CountryId == countryPrice.CountryId && cp.IsCurrent == true,cancellationToken);
            if (oldCountryPrice != null)
            {
                oldCountryPrice.IsCurrent = false;
                oldCountryPrice.ModificationDate=DateTime.Now;
                _context.Entry<CountryPrice>(oldCountryPrice).State = EntityState.Modified;
            }

            countryPrice.IsCurrent = true;
            countryPrice.CreationDate = DateTime.Now;
            var insertResult = await _context.CountryPrices.AddAsync(countryPrice,cancellationToken);
            return insertResult.Entity;
        }
        

        public async Task<CountryProductPrice> InsertCountryProductPriceAsync(CountryProductPrice countryProductPrice, CancellationToken cancellationToken)
        {


            var oldCountryProductPrice = await _context.CountryProductPrices.FirstOrDefaultAsync(cp => 
            cp.CountryId == countryProductPrice.CountryId &&
            cp.ProductTypeId == countryProductPrice.ProductTypeId,cancellationToken);
            if (oldCountryProductPrice != null)
            {
                
                await InsertProductPriceHistoryAsync(oldCountryProductPrice,cancellationToken);
                var result = UpdateProductPrice(oldCountryProductPrice,countryProductPrice);
                return result;
            }
            else
            {
                var result = await _context.CountryProductPrices.AddAsync(countryProductPrice, cancellationToken);
                return result.Entity;
            }


            
        }

        private async Task<CountryPriceHistory> InsertCountryPriceHistoryAsync(CountryPrice oldCountryPrice, CancellationToken cancellationToken) {
            CountryPriceHistory history = new () 
            {
                CountryPriceId = oldCountryPrice.Id,
                Kilometers = oldCountryPrice.Kilometers,
                Price = oldCountryPrice.Price,
                ExtraKilometers = oldCountryPrice.ExtraKilometers,
                ExtraKiloPrice = oldCountryPrice.ExtraKiloPrice,
                IsDeleted = oldCountryPrice.IsDeleted,
                CreatedBy = oldCountryPrice.ModifiedBy,
                CreationDate = DateTime.Now
                
            };

            var result = await _context.CountryPriceHistories.AddAsync(history,cancellationToken);
            return result.Entity;
        }

        private CountryPrice UpdateCountryPrice(CountryPrice oldCountryPrice , CountryPrice updatedCountryPrice)
        {
            oldCountryPrice.CountryId = updatedCountryPrice.CountryId;
            oldCountryPrice.Kilometers = updatedCountryPrice.Kilometers;
            oldCountryPrice.Price = updatedCountryPrice.Price;
            oldCountryPrice.ExtraKilometers = updatedCountryPrice.ExtraKilometers;
            oldCountryPrice.ExtraKiloPrice = updatedCountryPrice.ExtraKiloPrice;
            oldCountryPrice.IsDeleted = updatedCountryPrice.IsDeleted;
            oldCountryPrice.CreatedBy = updatedCountryPrice.CreatedBy;
            oldCountryPrice.ModifiedBy = updatedCountryPrice.ModifiedBy;
            oldCountryPrice.ModificationDate = DateTime.Now;
             _context.Entry<CountryPrice>(oldCountryPrice).State = EntityState.Modified;
            return updatedCountryPrice;
        }


        private async Task<CountryProductPriceHistory> InsertProductPriceHistoryAsync(CountryProductPrice oldCountryProductPrice, CancellationToken cancellationToken)
        {
            CountryProductPriceHistory history = new ()
            {
                CountryProductPriceId = oldCountryProductPrice.Id,
                Kilometers = oldCountryProductPrice.Kilometers,
                Price = oldCountryProductPrice.Price,
                ExtraKilometers = oldCountryProductPrice.ExtraKilometers,
                ExtraKiloPrice = oldCountryProductPrice.ExtraKiloPrice,
                IsDeleted = oldCountryProductPrice.IsDeleted,
                CreatedBy = oldCountryProductPrice.ModifiedBy,
                CreationDate = DateTime.Now
            };

            var result = await _context.CountryProductPriceHistories.AddAsync(history,cancellationToken);
            return result.Entity;
        }

        private CountryProductPrice UpdateProductPrice(CountryProductPrice oldCountryProductPrice, CountryProductPrice updatedCountryProductPrice)
        {
            oldCountryProductPrice.Kilometers = updatedCountryProductPrice.Kilometers;
            oldCountryProductPrice.Price = updatedCountryProductPrice.Price;
            oldCountryProductPrice.ExtraKilometers = updatedCountryProductPrice.ExtraKilometers;
            oldCountryProductPrice.ExtraKiloPrice = updatedCountryProductPrice.ExtraKiloPrice;
            oldCountryProductPrice.IsDeleted = false;
            oldCountryProductPrice.CreatedBy = updatedCountryProductPrice.CreatedBy;
            oldCountryProductPrice.ModifiedBy = updatedCountryProductPrice.ModifiedBy;
            oldCountryProductPrice.ModificationDate = DateTime.Now;
            _context.Entry<CountryProductPrice>(oldCountryProductPrice).State = EntityState.Modified;
            return updatedCountryProductPrice;
        }

        public async Task<List<City>> GetCitiesAsync(CancellationToken cancellationToken)
        {
            return await _context.Cities.ToListAsync(cancellationToken);
        }

        public async Task<City?> GetCityByIdAsync(long id, CancellationToken cancellationToken)
        {
            /*return await _context.Cities.Where(c => c.Id == ID).FirstOrDefaultAsync(cancellationToken);*/
            return await _context.Cities.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);
        }



        

        public async Task<IEnumerable<object>> GetCountriesCitiesAsync(CancellationToken cancellationToken)
        {

            /*var countriesTask = _context.Countries.ToListAsync(cancellationToken);
            var citieTask =  _context.Cities.ToListAsync(cancellationToken);

            await Task.WhenAll(countriesTask, citieTask);
            var countries = await countriesTask;
            var cities = await citieTask;*/

            /*return cities.GroupBy(city => city.CountryId)
                .Select( country_group =>  new { 
                    CountryId = (long)country_group.Key,
                    Country = countries.FirstOrDefault(c => c.Id == country_group.Key),
                    Cities = country_group
                });*/

            var countries = await _context.Countries.ToListAsync(cancellationToken);
            var cities = await _context.Cities.ToListAsync(cancellationToken);

            return from city in cities
                   group city by city.CountryId into country_group
                   select new
                   {
                       CountryId = country_group.Key,
                       Country = countries.FirstOrDefault(c => c.Id == country_group.Key),
                       Cities = country_group
                   };

        }



        public async Task<List<City>> GetCitiesByAsync(Expression<Func<City, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Cities.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<City> InsertCityAsync(City city, CancellationToken cancellationToken)
        {
            city.CreationDate = DateTime.Now;
            var insertResult = await _context.Cities.AddAsync(city,cancellationToken);
            return insertResult.Entity;
        }


        public async Task<City?> UpdateCityAsync(City city, CancellationToken cancellationToken) {
            var oldCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == city.Id,cancellationToken);
            if (oldCity == null) return null;

            oldCity.ArabicName = city.ArabicName;
            oldCity.Name = city.Name;
            oldCity.CountryId = city.CountryId;
            oldCity.ModificationDate = DateTime.Now;

            _context.Entry<City>(oldCity).State = EntityState.Modified;
            return oldCity;

        }


        public async Task<List<City>> InsertCitiesAsync(List<City> cities, CancellationToken cancellationToken)
        {
            cities.ForEach(c => c.CreationDate = DateTime.Now);
            await _context.Cities.AddRangeAsync(cities,cancellationToken);
            return cities;
        }

        public async Task<Country?> DeleteCountryAsync(long id, CancellationToken cancellationToken)
        {
            var oldCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);
            if (oldCountry == null) return null;


            var allCountryCities =  _context.Cities.Where(c => c.CountryId == oldCountry.Id);
            _context.Cities.RemoveRange(allCountryCities);
            _context.Countries.Remove(oldCountry);
            return oldCountry;

        }

        

        public async Task<City?> DeleteCityAsync(long id, CancellationToken cancellationToken)
        {
            var oldCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id,cancellationToken);
            if (oldCity == null) return null;

            _context.Cities.Remove(oldCity);
            return oldCity;
        }
    }
}
