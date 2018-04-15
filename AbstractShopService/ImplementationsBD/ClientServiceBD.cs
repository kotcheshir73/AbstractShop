using AbstractShopModel;
using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractShopService.ImplementationsBD
{
    public class ClientServiceBD : IClientService
    {
        private AbstractDbContext context;

        public ClientServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<ClientViewModel> GetList()
        {
            List<ClientViewModel> result = context.Clients
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    ClientFIO = rec.ClientFIO,
                    Mail = rec.Mail
                })
                .ToList();
            return result;
        }

        public ClientViewModel GetElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ClientViewModel
                {
                    Id = element.Id,
                    ClientFIO = element.ClientFIO,
                    Mail = element.Mail,
                    Messages = context.MessageInfos
                            .Where(recM => recM.ClientId == element.Id)
                            .Select(recM => new MessageInfoViewModel
                            {
                                MessageId = recM.MessageId,
                                DateDelivery = recM.DateDelivery,
                                Subject = recM.Subject,
                                Body = recM.Body
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.ClientFIO == model.ClientFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Clients.Add(new Client
            {
                ClientFIO = model.ClientFIO,
                Mail = model.Mail
            });
            context.SaveChanges();
        }

        public void UpdElement(ClientBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec =>
                                    rec.ClientFIO == model.ClientFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ClientFIO = model.ClientFIO;
            element.Mail = model.Mail;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
