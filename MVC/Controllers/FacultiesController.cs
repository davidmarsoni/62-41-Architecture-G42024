using DAL.Classes;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;
using MVC.Controllers.Util;
using MVC.Models;
using MVC.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly ILogger<FacultiesController> _logger;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private static List<UserViewModel> _selectedUsers = new List<UserViewModel>();

        public FacultiesController(ILogger<FacultiesController> logger, IUserService userService, IGroupService groupService, IAccountService accountService, ITransactionService transactionService)
        {
            _userService = userService;
            _groupService = groupService;
            _accountService = accountService;
            _transactionService = transactionService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupService.GetAllGroups();
            var users = await _userService.GetAllUsersActiveWithAccount();

            if (groups == null || users == null)
            {
                ToastrUtil.ToastrError(this, "Unable to fetch groups or users, please contact support");
                return RedirectToAction("Error", "Home");
            }

            ViewData["Groups"] = new SelectList(groups, nameof(GroupDTO.GroupId), nameof(GroupDTO.DisplayName));
            ViewData["Users"] = new SelectList(users, nameof(UserDTO.UserId), nameof(UserDTO.DisplayName));

            var model = new FacultiesViewModel
            {
                SelectedUsers = _selectedUsers
            };

            return View(model);
        }

        // GET: Faculties/ListOfUsersCheck
        public async Task<IActionResult> ListOfUsersCheck()
        { 
            List<UserViewModel> _selectedUsersCopy = new List<UserViewModel>();
            foreach (var user in _selectedUsers)
            {
                _selectedUsersCopy.Add(user);
            }
            _selectedUsers.Clear();
            return View(_selectedUsersCopy);
        }


        [HttpPost]
        public async Task<IActionResult> AddSelectedUser(int userId)
        {
            _logger.LogInformation("Received userId: {UserId}", userId);

            // Get the user from the database
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                ToastrUtil.ToastrError(this, "User not found");
                return BadRequest();
            }

            // Add the user to the selected users
            if(!_selectedUsers.Exists(u => u.UserId == user.UserId))
            {
                Boolean flag = await AddUserToSelectedUsers(user);
                if (flag)
                {
                    ToastrUtil.ToastrInfo(this, "User successfully added to selected users");
                }
            }else
            {
                ToastrUtil.ToastrWarning(this, "User already in selected users");
            }

            // Return the updated view
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddSelectedGroup(int groupId)
        {
            //get the group from the database for debugging purposes
            var group = await _groupService.GetGroupById(groupId);
            // Get the users from the group
            var users = await _userService.GetUsersByGroupId(groupId);

            if(users == null)
            {
                ToastrUtil.ToastrError(this, "The group"+ group.Acronym + "does not contain any users");
                return BadRequest();
            }

            // Add the users to the selected users
            foreach (var user in users)
            {
                if (!_selectedUsers.Exists(u => u.UserId == user.UserId))
                {
                    await AddUserToSelectedUsers(user);
                }
            }

            ToastrUtil.ToastrInfo(this, "User of the group successfully added to selected users");
            
            // Return the updated view
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteSelectedUser(int userId)
        {
            var user = _selectedUsers.Find(u => u.UserId == userId);
            if (user == null)
            {
                ToastrUtil.ToastrError(this, "User not found in selected users");
                return RedirectToAction("Index");
            }

            _selectedUsers.Remove(user);
            ToastrUtil.ToastrInfo(this, "User successfully removed from selected users");
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult ClearSelectedUsers()
        {
            _selectedUsers.Clear();
            ToastrUtil.ToastrInfo(this, "Selected users successfully cleared");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddQuotaToSelectedUsers([Bind("quota")] decimal quota)
        {
            //Test if there is at least one user selected
            if (_selectedUsers.Count == 0)
            {
                ToastrUtil.ToastrError(this, "No user selected");
                return RedirectToAction("Index");
            }

            //Test if the quota is valid (positive number below 1000)
            if (quota <= 0 || quota > 1000)
            {
                ToastrUtil.ToastrError(this, "Invalid quota The quota must be a positive number below 1000");
                return RedirectToAction("Index");
            }

            //Add the quota to the selected users
            foreach (var user in _selectedUsers)
            {
                TransactionHistoryDTO transaction = new TransactionHistoryDTO
                {
                    AccountId = user.AccountId,
                    Amount = quota,
                    DateTime = DateTime.Now,
                    Src = Src.Allocation,
                    TransactionType = TransactionType.AddCredit,
                    ConversionName = null,
                    ConversionValue = null
                };

                if (!await _transactionService.PostTransaction(transaction))
                {
                    ToastrUtil.ToastrError(this, "Unable to handle the transaction");
 
                    return RedirectToAction("Index");
                }
            }

            await UpdatesSelectedUsers();

            ToastrUtil.ToastrSuccess(this, "Successfully added the quota of " + quota + " CHF to the selected users");
            
            return View("ListOfUsersCheck", _selectedUsers);
        }
    
        private async Task<Boolean> AddUserToSelectedUsers(UserDTO user)
        {
            
            AccountDTO? account = await _accountService.GetAccountByUserId(user.UserId);
            IEnumerable<GroupDTO>? groups = await _groupService.GetGroupsByUserId(user.UserId);

            if (account != null)
            {
                var userViewModel = new UserViewModel(user, account, groups.ToList());

                _selectedUsers.Add(userViewModel);
                return true;
            }
            else { 
                ToastrUtil.ToastrWarning(this, "User"+ user.Username + " does not have an account");
                return false;
            }
        }


        private async Task UpdatesSelectedUsers()
        {
            foreach (var user in _selectedUsers)
            {
                AccountDTO? account = await _accountService.GetAccountByUserId(user.UserId);
                IEnumerable<GroupDTO>? groups = await _groupService.GetGroupsByUserId(user.UserId);
                if (account != null && groups != null)
                { 
                    user.Balance = account.Balance;
                    user.Groups = groups.ToList();
                }
            }
        }
    }
}