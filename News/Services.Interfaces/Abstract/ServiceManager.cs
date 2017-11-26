﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Services;
using Domain.Interfaces;
using Services.Interfaces.Impl;

namespace Data
{
    public class ServiceManager : IServiceManager
    {
        private SentimeContext _context;
        private IOrganisationService _organisationService;
        private IAppUserService _appUserService;
        private INewsService _newsService;
        private ICategories _categoriesService;

        public SentimeContext DbContext => _context ?? (_context = new SentimeContext());

        public IOrganisationService OrganisationService => _organisationService ?? (_organisationService = new OrganisationService(DbContext));
        public IAppUserService AppUserService => _appUserService ?? (_appUserService = new AppUserService(DbContext));
        public INewsService NewsService => _newsService ?? (_newsService = new NewsService(DbContext));
        public ICategories CategoriesService => _categoriesService ?? (_categoriesService = new CategoriesService(DbContext));
    }
}
