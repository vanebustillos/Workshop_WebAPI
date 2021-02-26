using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Exceptions;
using Database;
using Database.Exceptions;

namespace BusinessLogic
{
    public class WorkshopLogic : IWorkshopLogic
    {
        private IWorkshopTableDB _workshopDB;
        private List<Workshop> allWorkshop;
        private const string _unvalidInitialWorkshop = "Workshop-0";
        private const string _workshopIdPrefix = "Workshop-";
        private const string _firstWorkshop = "Workshop-1";
        private const string _postponed = "Postponed";
        private const string _cancelled = "Cancelled";

        public WorkshopLogic(IWorkshopTableDB workshopDB)
        {
            _workshopDB = workshopDB;
            allWorkshop = _workshopDB.GetAll();
        }

        public List<WorkshopDTO> Get() 
        { 
            UpdateLocalDB();

            if(allWorkshop?.Any() != true)
            {
                throw new EmptyDatabaseException("Tha database is empty.");
            }

            List<WorkshopDTO> workshopList = new List<WorkshopDTO>();
            foreach (Workshop workshop in allWorkshop)
            {
                workshopList.Add(ConvDBtoDTO(workshop));
            }
            return workshopList;
        }

        public WorkshopDTO Post(WorkshopDTO workshop)
        {
            if (!ValidateStringInput(workshop.Name))
            {
                throw new InvalidWorkshopNameException(404, "The input name is empty or null");
            }

            if (!ValidateStringInput(workshop.Status))
            {
                throw new InvalidWorkshopStatusException(404, "The input status is empty or null");
            }

            UpdateLocalDB();
       
            Workshop input = ConvDTOtoDB(workshop);
            input.Id = GenerateId();
            _workshopDB.Create(input);
            return  workshop;
        }

        public WorkshopDTO Put(WorkshopDTO workshopToUpdate, string workshopId)
        {
            if (!MatchedId(workshopId))
            {
                throw new WorkshopNotFoundException(400, "There isn't any workshop matched.");
            }

            if (!ValidateStringInput(workshopToUpdate.Name))
            {
                throw new InvalidWorkshopNameException(404, "The input name is empty or null");
            }

            if (!ValidateStringInput(workshopToUpdate.Status))
            {
                throw new InvalidWorkshopStatusException(404, "The input status is empty or null");
            }

            WorkshopDTO updatedWorkshop = new WorkshopDTO();
            Workshop updatedWorkshopDB = new Workshop();

            UpdateLocalDB();

            allWorkshop.Where(workshop => workshop.Id.Equals(workshopId))
              .Select(workshop => {
                  workshop.Name = workshopToUpdate.Name;
                  workshop.Status = workshopToUpdate.Status;

                  updatedWorkshopDB = ConvDTOtoDB(workshopToUpdate);
                  _workshopDB.Update(updatedWorkshopDB);
                  updatedWorkshop = ConvDBtoDTO(updatedWorkshopDB);
                  updatedWorkshop.Id = workshop.Id;
                  return workshop;
              })
              .ToList();

            return updatedWorkshop;
        }
        public WorkshopDTO Delete(string workshopId)
        {
            if (!MatchedId(workshopId))
            {
                throw new WorkshopNotFoundException(400, "There isn't any workshop matched.");
            }

            WorkshopDTO deletedWorkshop = new WorkshopDTO();
            Workshop deletedFromDB = new Workshop();

            UpdateLocalDB();
            allWorkshop.Where(workshop => workshop.Id.Equals(workshopId))
              .Select(workshop => {
                  deletedFromDB = _workshopDB.Delete(workshop);
                  allWorkshop.Remove(workshop);
                  deletedWorkshop = ConvDBtoDTO(deletedFromDB);
                  return workshop;
              })
              .ToList();

            return deletedWorkshop;
        }

        public WorkshopDTO Cancel(string workshopId)
        {
            if (!MatchedId(workshopId))
            {
                throw new WorkshopNotFoundException(400, "There isn't any workshop matched.");
            }

            WorkshopDTO cancelledWorkshop = new WorkshopDTO();
            Workshop updatedWorkshop = new Workshop();

            UpdateLocalDB();
            allWorkshop.Where(workshop => workshop.Id.Equals(workshopId))
              .Select(workshop => {
                  workshop.Status = _cancelled;
                  updatedWorkshop = _workshopDB.Update(workshop);
                  cancelledWorkshop = ConvDBtoDTO(updatedWorkshop);
                  return workshop;
              })
              .ToList();
   
            return cancelledWorkshop;
        }

        public WorkshopDTO Postpone(string workshopId)
        {
            if (!MatchedId(workshopId))
            {
                throw new WorkshopNotFoundException(400, "There isn't any workshop matched.");
            }

            WorkshopDTO posponedWorkshop = new WorkshopDTO();
            Workshop updatedWorkshop = new Workshop();

            UpdateLocalDB();

            allWorkshop.Where(workshop => workshop.Id.Equals(workshopId))
              .Select(workshop => {
                  workshop.Status = _postponed;
                  updatedWorkshop = _workshopDB.Update(workshop);
                  posponedWorkshop = ConvDBtoDTO(updatedWorkshop);
                  return workshop;
              })
              .ToList();

            return posponedWorkshop;
        }

        private string GenerateId()
        {
            string id;

            if (allWorkshop.Count == 0)
            {
                id = _firstWorkshop;
            }
            else
            {
                Workshop lastWorkshop = allWorkshop.Last();
                string[] fracment = lastWorkshop.Id.Split("-");
                int lastId = Int32.Parse(fracment[1]) + 1;
                id = $"{ _workshopIdPrefix }{ lastId }";
            }

            return id;
        }

        private void UpdateLocalDB()
        {
            allWorkshop= _workshopDB.GetAll();
        }

        private Workshop ConvDTOtoDB(WorkshopDTO oldWorkshop) //Converts a DTOWorkshop to a DB Workshop
        {
            Workshop validWorkshop = new Workshop();
            if (!oldWorkshop.Id.Equals(_unvalidInitialWorkshop)){
                validWorkshop.Id = oldWorkshop.Id;
            }
            validWorkshop.Name = oldWorkshop.Name;
            validWorkshop.Status = oldWorkshop.Status;

            return validWorkshop;
        }

        private WorkshopDTO ConvDBtoDTO(Workshop oldWorkshop) //Converts a DB Workshop to a DTOWorkshop
        {
            WorkshopDTO validWorkshop = new WorkshopDTO
            {
                Id = oldWorkshop.Id,
                Name = oldWorkshop.Name,
                Status = oldWorkshop.Status
            };

            return validWorkshop;
        }

        private bool ValidateStringInput(string input)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(input))
            {
                isValid = true;
            }

            return isValid;
        }
        private bool MatchedId(string id)
        {
            bool matchExists = false;

            if (!ValidateStringInput(id))
            {
                throw new InvalidWorkshopNameException(404, "The input ID is empty or null");
            }

            allWorkshop.Where(workshop => workshop.Id.Equals(id))
                 .Select(workshop => {
                     matchExists = true;
                     return workshop;
                 })
                 .ToList();

            return matchExists;
        }
    }
}