using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int aid, int uid, int value);
    }
    public class VotesRepository: IVotesRepository
    {
        StackOverflowDatabaseDbContext db;
        public VotesRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }
        public void UpdateVote(int aid, int uid, int value)
        {
            int UpdateValue;
            if (value > 0)
            {
                UpdateValue = 1;
            }

            else if (value < 0)
            {
                UpdateValue = -1;
            }
            else 
            {
                UpdateValue = 0;
            }

            Vote vote = db.Votes.Where(s => s.AnswerID == aid && s.UserID==uid).FirstOrDefault();
            if(vote!=null)
            {
                vote.VotesValue = UpdateValue;
            }
            else
            {
                Vote newVote = new Vote() { AnswerID=aid,UserID=uid,VotesValue=UpdateValue};
                db.Votes.Add(newVote);
            }
            db.SaveChanges();

        }
    }
}
