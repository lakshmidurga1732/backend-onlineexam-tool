﻿using OnlineExam1.DTO;
using OnlineExam1.Entity;

namespace OnlineExam1.Repo
{
    public class UserResponseRepoImpl : IRepo<UserResponseDTO>
    {
        private readonly MyContext context;

        public UserResponseRepoImpl(MyContext ctx)
        {
            context = ctx;
        }

        public bool Add(UserResponseDTO item)
        {
            UserResponse userResponse = new UserResponse
            {
                TestID = item.TestID,
                QuestionID = item.QuestionID,
                UserId = item.UserId, 
                UserAnswer = item.UserAnswer
            };

            context.UserResponses.Add(userResponse);
            context.SaveChanges();
            return true;
        }

        public List<UserResponseDTO> GetAll()
        {
            var result = context.UserResponses
                .Select(u => new UserResponseDTO
                {
                    ResponseID = u.ResponseID,
                    TestID = u.TestID,
                    QuestionID = u.QuestionID,
                    UserId = u.UserId, 
                    UserAnswer = u.UserAnswer
                })
                .ToList();

            return result;
        }

        public UserResponseDTO GetById(int responseId)
        {
            var userResponse = context.UserResponses
                .FirstOrDefault(u => u.ResponseID == responseId);

            if (userResponse == null)
            {
                return null;
            }

            return new UserResponseDTO
            {
                ResponseID = userResponse.ResponseID,
                TestID = userResponse.TestID,
                QuestionID = userResponse.QuestionID,
                UserId = userResponse.UserId, 
                UserAnswer = userResponse.UserAnswer
            };
        }

        public bool Update(int responseId, UserResponseDTO updatedItem)
        {
            var userResponse = context.UserResponses.Find(responseId);

            if (userResponse == null)
            {
                return false; 
            }

            userResponse.TestID = updatedItem.TestID;
            userResponse.QuestionID = updatedItem.QuestionID;
            userResponse.UserId = updatedItem.UserId; 
            userResponse.UserAnswer = updatedItem.UserAnswer;

            context.SaveChanges();
            return true;
        }

        public bool Delete(int responseId)
        {
            var userResponse = context.UserResponses.Find(responseId);

            if (userResponse == null)
            {
                return false; 
            }

            context.UserResponses.Remove(userResponse);
            context.SaveChanges();
            return true;
        }
    }
}
