using System;
using System.Collections.Generic;
using workshop_web_api.Database;

namespace workshop_web_api.Database
{
    public class WorkshopTableDB : IWorkshopTableDB
    {
        List<Workshop> DataBase = new List<Workshop>
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
            return DataBase;
        }

        public Workshop Create(Workshop newWorkshop)
        {
            DataBase.Add(newWorkshop);
            return newWorkshop;
        }

        public void Update(Workshop updatedWorkshop)
        {
            foreach (Workshop workshop in DataBase)
            {
                if (workshop.Id == updatedWorkshop.Id)
                {
                    workshop.Name = updatedWorkshop.Name;
                    workshop.Status = updatedWorkshop.Status;
                    break;
                }
            }
        }

        public void Delete(Workshop workshop)
        {
            DataBase.Remove(workshop);
        }
    }
}