using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using MVC.Services.Interfaces;
using DTO;
using MVC.Controllers.Util;
using X.PagedList;

namespace MVC.Controllers.Admin
{
    public class UserGroupsController : Controller
    {
        private readonly ILogger<UserGroupsController> _logger;
        private readonly IUserGroupService _userGroupService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public UserGroupsController(ILogger<UserGroupsController> logger, IUserGroupService userGroupService, IUserService userService, IGroupService groupService)
        {
            _logger = logger;
            _userGroupService = userGroupService;
            _userService = userService;
            _groupService = groupService;
        }


        // GET: UserGroups
        public async Task<IActionResult> Index(int? page, int pageSize = 10)
        {
            IEnumerable<UserGroupDTO>? userGroups = await _userGroupService.GetAllUserGroups();
            if (userGroups == null)
            {
                ToastrUtil.ToastrError(this, "Unable to fetch user groups, please contact support");
                return Redirect("/");
            }

            int pageNumber = page ?? 1;
            var pagedList = userGroups.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }

        // GET: UserGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            UserGroupDTO? userGroup = await _userGroupService.GetUserGroupById(id.Value);
            if (userGroup == null)
            {
                return userGroupNotFound();
            }

            return View(userGroup);
        }   

        // GET: UserGroups/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GroupId"] = new SelectList(await _groupService.GetAllGroups(), nameof(GroupDTO.GroupId), nameof(GroupDTO.DisplayName));
            ViewData["UserId"] = new SelectList(await _userService.GetAllUsers(), nameof(UserDTO.UserId), nameof(UserDTO.DisplayName));
            return View();
        }

        // POST: UserGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,GroupId")] UserGroupDTO userGroup)
        {
            ModelState.Remove(nameof(UserGroupDTO.UserDisplayName));
            ModelState.Remove(nameof(UserGroupDTO.GroupDisplayName));

            if(ModelState.IsValid)
            {
                if (await _userGroupService.CreateUserGroup(userGroup) == null)
                {
                    ToastrUtil.ToastrError(this, "Unable to create user group, please contact support");
                    return RedirectToAction(nameof(Index));
                }
                ToastrUtil.ToastrSuccess(this, "User group created successfully");
                return RedirectToAction(nameof(Index));
            }

           
            return View(userGroup);
        }   

        // GET: UserGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            UserGroupDTO? userGroup = await _userGroupService.GetUserGroupById(id.Value);
            if (userGroup == null)
            {
                return userGroupNotFound();
            }

            return View(userGroup);
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _userGroupService.DeleteUserGroup(id))
            {
                ToastrUtil.ToastrError(this, "Unable to delete user group, please contact support");
                return RedirectToAction(nameof(Index));
            }
            ToastrUtil.ToastrSuccess(this, "User group deleted successfully");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult idNotProvided()
        {
            ToastrUtil.ToastrError(this, "Id of the user group was not provided");
            return NotFound();
        }

        private IActionResult userGroupNotFound()
        {
            ToastrUtil.ToastrError(this, "User group not found");
            return NotFound();
        }
    }
}
