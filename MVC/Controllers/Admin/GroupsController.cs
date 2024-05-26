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
using MVC.Services;

namespace MVC.Controllers.Admin
{
    public class GroupsController : Controller
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _groupService;

        public GroupsController(ILogger<GroupsController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            IEnumerable<GroupDTO>? groups = await _groupService.GetAllGroups();
            if (groups == null)
            {
                ToastrUtil.ToastrError(this, "Unable to fetch groups, please contact support");
                return Redirect("/");
            }
            return View(groups);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var group = await _groupService.GetGroupById(id.Value);

            if (group == null)
            {
                return groupNotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,Name,Acronym,IsDeleted")] GroupDTO group)
        {
            if (ModelState.IsValid)
            {
                if (await _groupService.CreateGroup(group) == null)
                {
                    ToastrUtil.ToastrError(this, "Unable to create group");
                    return View(group);
                }
                // redirect to the new account page
                ToastrUtil.ToastrSuccess(this, "Group successfully created");
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var group = await _groupService.GetGroupById(id.Value);

            if (group == null)
            {
                return groupNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,Name,Acronym,IsDeleted")] GroupDTO group)
        {
            if (id != group.GroupId)
            {
                ToastrUtil.ToastrError(this, "An error has occured with the edit of groups, please contact support");
                return View(group);
            }

            //remove the UserName from the model state
            if (ModelState.IsValid)
            {
                if (!await _groupService.UpdateGroup(group))
                {
                    ToastrUtil.ToastrError(this, "Group update failed");
                    return RedirectToAction(nameof(Index));
                }
                ToastrUtil.ToastrSuccess(this, "Group successfully updated");
                return RedirectToAction(nameof(Index));
            }

            return View(group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var group = await _groupService.GetGroupById(id.Value);

            if (group == null)
            {
                return groupNotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _groupService.DeleteGroup(id))
            {
                ToastrUtil.ToastrSuccess(this, "Group successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            ToastrUtil.ToastrError(this, "Group deletion failed");
            return RedirectToAction(nameof(Delete), id);
        }

        private IActionResult idNotProvided()
        {
            ToastrUtil.ToastrError(this, "Id was not provided");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult groupNotFound()
        {
            ToastrUtil.ToastrError(this, "Group not found");
            return RedirectToAction(nameof(Index));
        }
    }
}
