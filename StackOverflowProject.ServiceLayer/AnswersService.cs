using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;
using StackOverflowProject.Repositories;
using StackOverflowProject.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;
using System.CodeDom.Compiler;

namespace StackOverflowProject.ServiceLayer
{

    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel a);
        void UpdateAnswer(EditAnswerViewModel a);
        void UpdateAnswerVotesCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<AnswerViewModel> GetAnswersByQuestionID(int qid);
        List<AnswerViewModel> GetAnswersByAnswerID(int aid);
    }


    public class AnswersService:IAnswersService
    {
        IAnswersRepository ar;

        public AnswersService()
        {
            ar = new AnswersRepository();
        }

        public void InsertAnswer(NewAnswerViewModel a)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer ans = mapper.Map<NewAnswerViewModel,Answer>(a);
            ar.InsertAnswer(ans);
        }

        public void UpdateAnswer(EditAnswerViewModel a)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer ans = mapper.Map<EditAnswerViewModel, Answer>(a);
            ar.UpdateAnswer(ans);
        }
        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            ar.UpdateAnswerVotesCount(aid, uid, value);
        }
        public void DeleteAnswer(int aid)
        {
            ar.DeleteAnswer(aid);
        }
        public List<AnswerViewModel> GetAnswersByQuestionID(int qid)
        {
            List<Answer> ans=ar.GetAnswersByQuestionID(qid);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<List<Answer>, List<AnswerViewModel>>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> avm = mapper.Map<List<Answer>, List<AnswerViewModel>>(ans);
            return avm;
        }
        public List<AnswerViewModel> GetAnswersByAnswerID(int aid)
        {
            List<Answer> ans = ar.GetAnswersByAnswerID(aid);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<List<Answer>, List<AnswerViewModel>>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> avm = mapper.Map<List<Answer>, List<AnswerViewModel>>(ans);
            return avm;
        }

    }
}
