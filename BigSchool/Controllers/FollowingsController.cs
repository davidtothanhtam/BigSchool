using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;



namespace BigSchool.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbConText;

        public FollowingsController()
        {
            _dbConText = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbConText.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
                return BadRequest("Following already exists!");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };

            _dbConText.Followings.Add(following);
            _dbConText.SaveChanges();

            //following = _dbConText.Followings
            //    .Where(x => x.FolloweeId == followingDto.FolloweeId && x.FollowerId == userId)
            //    .Include(x => x.Followee)
            //    .Include(x => x.Follower).SingleOrDefault();

            //var followingNotification = new FollowingNotification()
            //{
            //    Id = 0,
            //    Logger = following.Follower.Name + " following " + following.Followee.Name
            //};
            //_dbConText.FollowingNotifications.Add(followingNotification);
            //_dbConText.SaveChanges();

            return Ok();
        }

        //[HttpDelete]
        //public IHttpActionResult UnFollow(string followeeId, string followerId)
        //{
        //    var follow = _dbConText.Followings
        //        .Where(x => x.FolloweeId == followeeId && x.FollowerId == followerId)
        //        .Include(x => x.Followee)
        //        .Include(x => x.Follower).SingleOrDefault();

        //    var followingNotification = new FollowingNotification()
        //    {
        //        Id = 0,
        //        Logger = follow.Follower.Name + " unfollow " + follow.Followee.Name
        //    };

        //    _dbConText.FollowingNotifications.Add(followingNotification);

        //    _dbConText.Followings.Remove(follow);
        //    _dbConText.SaveChanges();
        //    return Ok();
        //}
    }
}
