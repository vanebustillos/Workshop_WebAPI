using System;
using System.Collections.Generic;
using System.Linq;
using Database;

namespace Database
{
    public class WorkshopTableDB : IWorkshopTableDB
    {
        List<Workshop> _database = new List<Workshop>
            {
                    new Workshop() { Id = "Workshop-1",Name = "Introduccion Internal Apps", Status = "Done"},
                    new Workshop() { Id = "Workshop-2",Name = "OOP", Status = "Done"},
                    new Workshop() { Id = "Workshop-3",Name = "Branching Model and Versioning", Status = "Done"},
                    new Workshop() { Id = "Workshop-4",Name = "API + Microservices + Arq. Monolitica", Status = "Done"},
                    new Workshop() { Id = "Workshop-5",Name = "Git Dojo", Status = "Done"},
                    new Workshop() { Id = "Workshop-6",Name = "IT", Status = "Done"},
                    new Workshop() { Id = "Workshop-7",Name = "Soft Skills", Status = "Done"},
                    new Workshop() { Id = "Workshop-8",Name = "API .NET Practice", Status = "Done"},
                    new Workshop() { Id = "Workshop-9",Name = "Soft Skills and Rules", Status = "Done"},
                    new Workshop() { Id = "Workshop-10",Name = "Bases de Datos", Status = "Scheduled"},
                    new Workshop() { Id = "Workshop-11",Name = "Calidad en Software Comercial", Status = "Scheduled"}
            };

        public List<Workshop> GetAll()
        {
            return _database;
        }

        public Workshop Create(Workshop newWorkshop)
        {
            _database.Add(newWorkshop);
            return newWorkshop;
        }

        public Workshop Update(Workshop workshopToUpdate)
        {
            Workshop _updatedWorkshop = new Workshop();
            _database.Where(w => w.Id.Equals(workshopToUpdate.Id))
               .Select(w => { 
                   w.Name = workshopToUpdate.Name; 
                   w.Status = workshopToUpdate.Status; 
                   _updatedWorkshop = w; 
                   return w; })
               .ToList();
            return _updatedWorkshop;
        }

        public Workshop Delete(Workshop workshopToDelete)
        {
            _database.Remove(workshopToDelete);
            return workshopToDelete;
        }
    }
}