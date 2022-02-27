using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TreePorts.DTO;
using TreePorts.Interfaces.Services;
using TreePorts.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TreePorts.Services.Background;
public class RMQReceiverService : BackgroundService
{
    private IServiceProvider _sp;
    private IRMQService _rmqService;
    private IModel _newUserChannel;
    private IModel _updateUserChannel;
    private IModel _deleteUserChannel;

    public RMQReceiverService(IServiceProvider sp, IRMQService rmqService)
    {
        _sp = sp;
        _rmqService = rmqService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // when the service is stopping
        // dispose these references
        // to prevent leaks
        if (stoppingToken.IsCancellationRequested)
        {
            Dispose();
            return Task.CompletedTask;
        }

        ConsumeNewUser();
        ConsumeUpdateUser();
        ConsumeDeleteUser();

        return Task.CompletedTask;
    }


    private void ConsumeNewUser() 
    {
        var queueName = nameof(RabbitMQQueues.CoreServiceNewUser);
        _newUserChannel = _rmqService.consume(queueName);
        var consumer = new EventingBasicConsumer(_newUserChannel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("consumeNewUser  received: " + message);

            // should change to each Service class 
            Task.Run(async () => {

                using (var scope = _sp.CreateScope())
                {
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var userData = JsonConvert.DeserializeObject<RabbitMQUser>(message);

                    if (userData.UserTypeId == (int)UserTypes.Admin)
                    {
                        await HandleSavingAdminUser(_unitOfWork, userData);
                    }

                    if (userData.UserTypeId == (int)UserTypes.Support)
                    {
                        await HandleSavingSupportUser(_unitOfWork, userData);
                    }

                    if (userData.UserTypeId == (int)UserTypes.Captain)
                    {

                        await HandleSavingCaptainUser(_unitOfWork, userData);
                    }

                    if (userData.UserTypeId == (int)UserTypes.Agent)
                    {
                        await HandleSavingAgentUser(_unitOfWork, userData);
                    }

                    var result = await _unitOfWork.Save();
                    _newUserChannel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            });

             
        };
        _newUserChannel.BasicConsume(queue: queueName,
                             autoAck: false,
                             consumer: consumer);

    }

    private void ConsumeUpdateUser()
    {
        var queueName = nameof(RabbitMQQueues.CoreServiceUpdateUser);
        _updateUserChannel = _rmqService.consume(queueName);
        var consumer = new EventingBasicConsumer(_updateUserChannel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("ConsumeUpdateUser  received: " + message);

            // should change to each Service class 
            Task.Run(async () => {

                using (var scope = _sp.CreateScope())
                {
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var userData = JsonConvert.DeserializeObject<RabbitMQUser>(message);

                    if (userData.UserTypeId == (int)UserTypes.Admin)
                    {
                        await HandleUpdateAdminUser(_unitOfWork, userData);
                    }

                    if (userData.UserTypeId == (int)UserTypes.Support)
                    {
                        await HandleUpdateSupportUser(_unitOfWork, userData);
                    }

                    if (userData.UserTypeId == (int)UserTypes.Captain)
                    {

                        await HandleUpdateCaptainUser(_unitOfWork, userData);
                    }

                    if (userData.UserTypeId == (int)UserTypes.Agent)
                    {
                        await HandleUpdateAgentUser(_unitOfWork, userData);
                    }

                    var result = await _unitOfWork.Save();
                    _updateUserChannel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            });
        };
        _updateUserChannel.BasicConsume(queue: queueName,
                             autoAck: false,
                             consumer: consumer);
    }

    private void ConsumeDeleteUser()
    {
        var queueName = nameof(RabbitMQQueues.CoreServiceDeleteUser);
        _deleteUserChannel = _rmqService.consume(queueName);
        var consumer = new EventingBasicConsumer(_deleteUserChannel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("ConsumeDeleteUser  received: " + message);

            // should change to each Service class 
            Task.Run(async () => {

                using (var scope = _sp.CreateScope())
                {
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var userData = JsonConvert.DeserializeObject<RabbitMQUser>(message);

                    if (userData.UserTypeId == (int)UserTypes.Admin)
                    {
                        await HandleDeleteAdminUser(_unitOfWork, userData);
                    }

                    if (userData.UserTypeId == (int)UserTypes.Support)
                    {
                        await HandleDeleteSupportUser(_unitOfWork, userData);
                    }

                    if (userData.UserTypeId == (int)UserTypes.Captain)
                    {

                        await HandleDeleteCaptainUser(_unitOfWork, userData);
                    }

                    if (userData.UserTypeId == (int)UserTypes.Agent)
                    {
                        await HandleDeleteAgentUser(_unitOfWork, userData);
                    }

                    var result = await _unitOfWork.Save();
                    _deleteUserChannel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            });
        };
        _deleteUserChannel.BasicConsume(queue: queueName,
                             autoAck: false,
                             consumer: consumer);
    }


    private void Dispose() 
    {
        if(_newUserChannel != null )
            _newUserChannel.Dispose();

        if (_updateUserChannel != null)
            _updateUserChannel.Dispose();

        if (_deleteUserChannel != null)
            _deleteUserChannel.Dispose();

        _rmqService.dispose();
    }


    private CaptainUser ConvertRabbitMQUserToCaptainUser(RabbitMQUser rabbitMQUser) 
    {
        long Id = -1;
        //long.TryParse(rabbitMQUser.CoreUserId, out Id);
        var user = new CaptainUser
        {
            Id = rabbitMQUser.CoreUserId,
            CityId = rabbitMQUser.CityId,
            CountryId = rabbitMQUser.CountryId,
            Gender = rabbitMQUser.Gender.ToLower(),
            FirstName = rabbitMQUser.FirstName,
            LastName = rabbitMQUser.FamilyName,
            Mobile = rabbitMQUser.Mobile,
            StcPay = rabbitMQUser.StcPay,
            NationalNumber = rabbitMQUser.NationalNumber,
            PersonalImage = rabbitMQUser.Image,
            //StatusTypeId = rabbitMQUser.Status.StatusTypeId
        };
        return user;
    }


    private AdminUser ConvertRabbitMQUserToAdminUser(RabbitMQUser rabbitMQUser)
    {
        //long Id = -1;
        //long.TryParse(rabbitMQUser.CoreUserId, out Id);
        var user = new AdminUser
        {
            Id  = rabbitMQUser.CoreUserId, 
            CityId = rabbitMQUser.CityId,
            CountryId = rabbitMQUser.CountryId,
            Gender = rabbitMQUser.Gender,
            FirstName = rabbitMQUser.FirstName,
            LastName = rabbitMQUser.FamilyName,
            Mobile = rabbitMQUser.Mobile,
            NationalNumber = rabbitMQUser.NationalNumber,
            PersonalImage = rabbitMQUser.Image,
            //Email = rabbitMQUser.Email,
           // Password = rabbitMQUser.Password,
            //CurrentStatusId = rabbitMQUser.Status.StatusTypeId
        };
        return user;
    }


    private SupportUser ConvertRabbitMQUserToSupportUser(RabbitMQUser rabbitMQUser)
    {
        long Id = -1;
        long.TryParse(rabbitMQUser.CoreUserId, out Id);
        var user = new SupportUser
        {
            Id = rabbitMQUser.CoreUserId,
            CityId = rabbitMQUser.CityId,
            CountryId = rabbitMQUser.CountryId,
            Gender = rabbitMQUser.Gender,
            FirstName = rabbitMQUser.FirstName,
            LastName = rabbitMQUser.FamilyName,
            Mobile = rabbitMQUser.Mobile,
            NationalNumber = rabbitMQUser.NationalNumber,
            PersonalImage = rabbitMQUser.Image,
            //Email = rabbitMQUser.Email,
            //Password = rabbitMQUser.Password,
            //CurrentStatusId = rabbitMQUser.Status.StatusTypeId
        };
        return user;
    }

    private Agent ConvertRabbitMQUserToAgentUser(RabbitMQUser rabbitMQUser)
    {
        long Id = -1;
        long.TryParse(rabbitMQUser.CoreUserId, out Id);
        var user = new Agent
        {
            Id = rabbitMQUser.CoreUserId,
            CityId = rabbitMQUser.CityId,
            CountryId = rabbitMQUser.CountryId,
            Address = rabbitMQUser.Address,
            Fullname = rabbitMQUser.Fullname,
            Mobile = rabbitMQUser.Mobile,
            Email = rabbitMQUser.Email,
            Image = rabbitMQUser.Image,
           // CurrentStatusId = rabbitMQUser.Status.StatusTypeId
        };
        return user;
    }


    private async Task HandleSavingCaptainUser(IUnitOfWork _unitOfWork, RabbitMQUser userData) 
    {
        var user = ConvertRabbitMQUserToCaptainUser(userData);
        var insertedUser = await _unitOfWork.CaptainRepository.InsertCaptainUserAsync(user);
        var result = await _unitOfWork.Save();

        if (result <= 0) return;

        userData.CoreUserId = insertedUser.Id.ToString(); 
        var account = new CaptainUserAccount
        {
            CaptainUserId = insertedUser.Id,
            Mobile = insertedUser.Mobile,
            //PersonalImage = user.PersonalImage,
            //Fullname = user.FirstName + " " + user.FamilyName,
            StatusTypeId = (long)userData.Status.StatusTypeId,
            CreationDate = DateTime.Now
        };

        var userAccount = await _unitOfWork.CaptainRepository.InsertCaptainUserAccountAsync(account);
        CaptainUserCurrentStatus userStatus_Review = new CaptainUserCurrentStatus()
        {
            CaptainUserAccountId = userAccount.Id,
            StatusTypeId = (long)StatusTypes.Reviewing,
            IsCurrent = true,
            CreationDate = DateTime.Now
        };
        var insertedUserCurrentStatusResult = await _unitOfWork.CaptainRepository.InsertCaptainUserCurrentStatusAsync(userStatus_Review);
        result = await _unitOfWork.Save();

        if (result <= 0) return;

        string messageData = Utility.ConvertToJson(userData);
        string queueName = nameof(RabbitMQQueues.CoreServiceUpdateUser);
        _rmqService.send(queueName, messageData);
    }



    private async Task HandleSavingAdminUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        var user = ConvertRabbitMQUserToAdminUser(userData);
        byte[] passwordHash, passwordSalt;
        var password = "";// user.Password;
        Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        AdminUserAccount account = new ()
        {
            Email = userData.Email.ToLower(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            StatusTypeId = (long)userData.Status.StatusTypeId,
            CreationDate = DateTime.Now
        };
        if (user.AdminUserAccounts == null) user.AdminUserAccounts = new List<AdminUserAccount>();
        user.AdminUserAccounts.Add(account);
        //user.CurrentStatusId = (long)userData.Status.StatusTypeId;
        user = await _unitOfWork.AdminRepository.InsertAdminUserAsync(user);
        var result = await _unitOfWork.Save();
        if (result <= 0) return;

        AdminCurrentStatus adminCurrentStatus = new ()
        {
            AdminUserAccountId = account.Id,
            StatusTypeId = (long)userData.Status.StatusTypeId,
            IsCurrent = true,
            CreationDate = DateTime.Now,
        };


        var insertStatusResult = await _unitOfWork.AdminRepository.InsertAdminCurrentStatusAsync(adminCurrentStatus);
        result = await _unitOfWork.Save();
        if (result == 0) return;

        userData.CoreUserId = user.Id.ToString();
        string messageData = Utility.ConvertToJson(userData);
        string queueName = nameof(RabbitMQQueues.CoreServiceUpdateUser);
        _rmqService.send(queueName, messageData);
    }




    private async Task HandleSavingSupportUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        var user = ConvertRabbitMQUserToSupportUser(userData);
        //user.Email = user.Email.ToLower();
        byte[] passwordHash, passwordSalt;
        var password = ""; // Utility.GeneratePassword();
        Utility.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        SupportUserAccount account = new SupportUserAccount()
        {
            Email = userData.Email.ToLower(),
            //Token = user.Token,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            StatusTypeId = (long)userData.Status.StatusTypeId
        };

        if (user.SupportUserAccounts == null) user.SupportUserAccounts = new List<SupportUserAccount>();
        user.SupportUserAccounts.Add(account);
        user = await _unitOfWork.SupportRepository.InsertSupportUserAsync(user);
        var result = await _unitOfWork.Save();
        if (result == 0) return;

        SupportUserCurrentStatus supportUserCurrentStatus = new SupportUserCurrentStatus()
        {
            SupportUserAccountId = account.Id,
            StatusTypeId = (long)userData.Status.StatusTypeId,
            IsCurrent = true,
            CreationDate = DateTime.Now,
        };

        var insertStatusResult = await _unitOfWork.SupportRepository.InsertSupportUserCurrentStatusAsync(supportUserCurrentStatus);
        result = await _unitOfWork.Save();
        if (result <= 0) return;

        userData.CoreUserId = user.Id.ToString();
        string messageData = Utility.ConvertToJson(userData);
        string queueName = nameof(RabbitMQQueues.CoreServiceUpdateUser);
        _rmqService.send(queueName, messageData);
    }




    private async Task HandleSavingAgentUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        var agent = ConvertRabbitMQUserToAgentUser(userData);
        agent.StatusTypeId = (long)userData.Status.StatusTypeId;
        agent.Email = agent.Email.ToLower();
        var insertResult = await _unitOfWork.AgentRepository.InsertAgentAsync(agent);
        var result = await _unitOfWork.Save();

        if (result <= 0) return;


        AgentCurrentStatus newAgentCurrentStatus = new ()
        {
            AgentId = insertResult.Id,
            StatusTypeId = (long)StatusTypes.New,
            IsCurrent = false,
            CreationDate = DateTime.Now
        };

        AgentCurrentStatus incompleteAgentCurrentStatus = new ()
        {
            AgentId = insertResult.Id,
            StatusTypeId = (long)userData.Status.StatusTypeId,
            IsCurrent = true,
            CreationDate = DateTime.Now
        };

        

        var newAgentStatusInsertedResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(newAgentCurrentStatus);
        var incompleteAgentStatusInsertedResult = await _unitOfWork.AgentRepository.InsertAgentCurrentStatusAsync(incompleteAgentCurrentStatus);

        result = await _unitOfWork.Save();
        if (result <= 0) return;

        userData.CoreUserId = agent.Id.ToString();
        string messageData = Utility.ConvertToJson(userData);
        string queueName = nameof(RabbitMQQueues.CoreServiceUpdateUser);
        _rmqService.send(queueName, messageData);
    }


    private async Task HandleUpdateAdminUser(IUnitOfWork _unitOfWork, RabbitMQUser userData) 
    {
        try
        {
            var user = ConvertRabbitMQUserToAdminUser(userData);
            var userResult = await _unitOfWork.AdminRepository.UpdateAdminUserAsync(user);
            var result = await _unitOfWork.Save();
            if (result == 0) return;

            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    private async Task HandleUpdateSupportUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        try
        {
            var user = ConvertRabbitMQUserToSupportUser(userData);
            var userResult = await _unitOfWork.SupportRepository.UpdateSupportUserAsync(user);
            var result = await _unitOfWork.Save();
            if (result == 0) return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }



    private async Task HandleUpdateAgentUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        try
        {
            var user = ConvertRabbitMQUserToAgentUser(userData);
            var updateResult = await _unitOfWork.AgentRepository.UpdateAgentAsync(user);
            var result = await _unitOfWork.Save();
            if (result == 0) return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }



    private async Task HandleUpdateCaptainUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        try
        {
            var user = ConvertRabbitMQUserToCaptainUser(userData);
            var updateResult = await _unitOfWork.CaptainRepository.UpdateCaptainUserAsync(user);
            var result = await _unitOfWork.Save();
            if (result == 0) return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    private async Task HandleDeleteAdminUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        try
        {
            var user = ConvertRabbitMQUserToAdminUser(userData);
            var updateResult = await _unitOfWork.AdminRepository.DeleteAdminUserAsync(user.Id);
            var result = await _unitOfWork.Save();
            if (result == 0) return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task HandleDeleteSupportUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        try
        {
            var user = ConvertRabbitMQUserToSupportUser(userData);
            var updateResult = await _unitOfWork.SupportRepository.DeleteSupportUserAccountAsync(user.Id);
            var result = await _unitOfWork.Save();
            if (result == 0) return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task HandleDeleteAgentUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        try
        {
            var user = ConvertRabbitMQUserToAgentUser(userData);
            var updateResult = await _unitOfWork.AgentRepository.DeleteAgentAsync(user.Id);
            var result = await _unitOfWork.Save();
            if (result == 0) return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task HandleDeleteCaptainUser(IUnitOfWork _unitOfWork, RabbitMQUser userData)
    {
        try
        {
            var user = ConvertRabbitMQUserToCaptainUser(userData);
            var updateResult = await _unitOfWork.CaptainRepository.DeleteCaptainUserAccountAsync(user.Id);
            var result = await _unitOfWork.Save();
            if (result == 0) return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }



}

