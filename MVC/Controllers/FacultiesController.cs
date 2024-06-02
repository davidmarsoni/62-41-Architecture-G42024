using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private static List<UserViewModel> _selectedUsers = new List<UserViewModel>();

        public FacultiesController(ILogger<FacultiesController> logger, IUserService userService, IGroupService groupService, IAccountService accountService)
        {
            _userService = userService;
            _groupService = groupService;
            _accountService = accountService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupService.GetAllGroups();
            var users = await _userService.GetAllUsers();

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
                AddUserToSelectedUsers(user);
            }

            // Log the action
            ToastrUtil.ToastrSuccess(this, "User successfully added to selected users");

            // Return the updated view
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddSelectedGroup(int groupId)
        {
            // Get the users from the group
            var users = await _userService.GetUsersByGroupId(groupId);

            if(users == null)
            {
                ToastrUtil.ToastrError(this, "This group has no users or this group does not exist");
                return BadRequest();
            }

            // Add the users to the selected users
            foreach (var user in users)
            {
                if (!_selectedUsers.Exists(u => u.UserId == user.UserId))
                {
                    AddUserToSelectedUsers(user);
                }
            }

            ToastrUtil.ToastrSuccess(this, "Users of the goups successfully added to selected users");
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
            }

            _selectedUsers.Remove(user);
            ToastrUtil.ToastrSuccess(this, "User successfully removed from selected users");
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult ClearSelectedUsers()
        {
            _selectedUsers.Clear();
            ToastrUtil.ToastrSuccess(this, "Selected users successfully cleared");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddQuotaToSelectedUsers(int quota)
        {
            //TODO: Implement
            ToastrUtil.ToastrError(this, "Not implemented");
            return RedirectToAction("Index");
        }

        private async void AddUserToSelectedUsers(UserDTO user)
        {
            
            AccountDTO account = await _accountService.GetAccountByUserId(user.UserId);
            IEnumerable<GroupDTO> groups = await _groupService.GetGroupsByUserId(user.UserId);
            
            if (account == null && groups == null)
            {
               return;
            }

            ToastrUtil.ToastrSuccess(this, "User successfully added to selected users");
            var userViewModel = new UserViewModel(user,account,groups.ToList());

            _selectedUsers.Add(userViewModel);
        }


    }
}