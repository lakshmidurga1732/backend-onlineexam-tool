﻿using OnlineExam1.Entity;
using OnlineExam1.Repo;
namespace OnlineExam1.Repo
{
    public class UnitOfWork
    {
        private MyContext context = null;
        private OrganizationRepoImpl organizationRepoImpl = null;
        private SiteRepoImpl siteRepoImpl = null;
        private SubjectRepoImpl subjectRepoImpl = null;
        private QuestionBankRepoImpl questionBankRepoImpl =null;
        private TestStructureRepoImpl testStructureRepoImpl=null;
        private AssignedTestRepoImpl assignedTestRepoImpl = null;
        private UserResponseRepoImpl userResponseRepoImpl = null;
        public UnitOfWork(MyContext ctx)
        {
            context = ctx;
        }

        public OrganizationRepoImpl OrganizationRepoImplObject
        {
            get
            {
                if (organizationRepoImpl == null)
                    organizationRepoImpl = new OrganizationRepoImpl(context);
                return organizationRepoImpl;
            }
        }

        public SiteRepoImpl SiteRepoImplObject
        {
            get
            {
                if (siteRepoImpl == null)
                    siteRepoImpl = new SiteRepoImpl(context);
                return siteRepoImpl;
            }
        }

        public SubjectRepoImpl SubjectRepoImplObject
        {
            get
            {
                if (subjectRepoImpl == null)
                    subjectRepoImpl = new SubjectRepoImpl(context);
                return subjectRepoImpl;
            }
        }
        public QuestionBankRepoImpl QuestionBankRepoImplObject
        {
            get
            {
                if (questionBankRepoImpl == null)
                    questionBankRepoImpl = new QuestionBankRepoImpl(context);
                return questionBankRepoImpl;
            }
        }
        public TestStructureRepoImpl TestStructureRepoImplObject
        {
            get
            {
                if (testStructureRepoImpl == null)
                    testStructureRepoImpl = new TestStructureRepoImpl(context);
                return testStructureRepoImpl;
            }
        }
        public AssignedTestRepoImpl AssignedTestRepoImplObject
        {
            get
            {
                if (assignedTestRepoImpl == null)
                    assignedTestRepoImpl = new AssignedTestRepoImpl(context);
                return assignedTestRepoImpl;
            }
        }
        public UserResponseRepoImpl UserResponseRepoImplObject
        {
            get
            {
                if (userResponseRepoImpl == null)
                    userResponseRepoImpl = new UserResponseRepoImpl(context);
                return userResponseRepoImpl;
            }
        }

        public void SaveAll()
        {
            if (context != null)
            {
                context.SaveChanges();
            }
        }
    }
}



