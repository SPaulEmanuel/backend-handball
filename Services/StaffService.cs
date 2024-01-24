
﻿using aplicatieHandbal.Data;
using aplicatieHandbal.Models;
using aplicatieHandbal.Validators;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace aplicatieHandbal.Services
{
        public interface IStaffService
        {
            Task<List<StaffDto>> GetAllStaff();
            Task<List<Staff>> GetAllInfoStaff();
            Task<List<List<StaffDto>>> GetStaffByPosition();
            Task<Staff> AddStaff(Staff model);
            Task<Staff> GetStaffById(Guid id);
            Task<Staff> UpdateStaff(Guid id, Staff updatedStaff);
            Task<Staff> DeleteStaff(Guid id);
            Task<Staff> updateStaffPatch(Guid id, JsonPatchDocument updatedPlayerReq);
        }

        public class StaffService : IStaffService 
        {
            private readonly AplicatieDBContext _aplicatieDBContext;
           

            public StaffService(AplicatieDBContext dbContext)
            {
                _aplicatieDBContext = dbContext;
            

            }

            public async Task<Staff> AddStaff(Staff model)
            {
                var validator = new StaffValidator();
                validator.ValidateAndThrow(model);
                _aplicatieDBContext.Staff.Add(model);
                await _aplicatieDBContext.SaveChangesAsync();

                return model;
            }

            public async Task<Staff> DeleteStaff(Guid id)
            {
                var staff = await _aplicatieDBContext.Staff.FindAsync(id);
                if (staff is not null)
                {
                    _aplicatieDBContext.Staff.Remove(staff);
                    await _aplicatieDBContext.SaveChangesAsync();
                    return staff;
                }
                throw new Exception("Cannot delete id");
            }

            public async Task<List<Staff>> GetAllInfoStaff()
            {
                var staff = await _aplicatieDBContext.Staff.ToListAsync();
                return staff;
            }

            public async Task<List<StaffDto>> GetAllStaff()
            {
                var allStaff = await _aplicatieDBContext.Staff
                    .Select(staff => new StaffDto
                    {
                        Name = staff.Name,
                        Surname = staff.Surname,
                        ImageUrl = staff.ImageUrl
                    })
                    .ToListAsync();

                return allStaff;
            }
            // PlayerService.cs

            public async Task<List<List<StaffDto>>> GetStaffByPosition()
            {
            var staffByPosition = await _aplicatieDBContext.Staff
                .GroupBy(staff => staff.Position)
                .Select(group => group.Select(staff => new StaffDto
                    {
                        Name = staff.Name,
                        Surname = staff.Surname,
                        ImageUrl = staff.ImageUrl

                    }).ToList())
                .ToListAsync() ;

            return staffByPosition;
            }




            public async Task<Staff> GetStaffById(Guid id)
            {
                var staff = await _aplicatieDBContext.Staff.FirstOrDefaultAsync(x => x.StaffID == id);
                if (staff is not null)
                {
                    return staff;

                }
                throw new Exception("Staff not found");
            }
        public async Task<Staff> updateStaffPatch(Guid id, JsonPatchDocument updatedStaffPatch)
        {
            var staff = await _aplicatieDBContext.Staff.FindAsync(id);
            if (staff != null)
            {
                updatedStaffPatch.ApplyTo(staff);
                await _aplicatieDBContext.SaveChangesAsync();
                return staff;
            }
            throw new Exception("staff not found ");
        }

        public async Task<Staff> UpdateStaff(Guid id, Staff updateStaffReq)
            {

                var staff = await _aplicatieDBContext.Staff.FindAsync(id);
                if (staff is not null)
                {
                var validator = new StaffValidator();
                validator.ValidateAndThrow(updateStaffReq);
                staff.Name = updateStaffReq.Name;
                staff.Surname = updateStaffReq.Surname;
                staff.Position = updateStaffReq.Position;
                staff.ImageUrl = updateStaffReq.ImageUrl;

                    await _aplicatieDBContext.SaveChangesAsync();
                    return staff;
                }
                throw new Exception("ID not found");
            }

        }
    }
