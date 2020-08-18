using AutoMapper;
using DataAccess;
using DataAccess.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessAccess
{
    public class BlogService:IBlogService
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        public BlogService(IRepository<Blog> repository, IMapper mapper, ILog log)
        {
            _blogRepository = repository;
            _logger = LogManager.GetLogger(typeof(BlogService));
        }
        public List<Blog> GetAllBlogs()
        {
            var result = _blogRepository.GetAll().ToList();
            return result;
        }
    }
}
