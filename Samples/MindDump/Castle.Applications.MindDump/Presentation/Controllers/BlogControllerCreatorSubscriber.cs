// Copyright 2004-2008 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.Applications.MindDump.Presentation.Controllers
{
	using System;
	using System.Collections;

	using Castle.Core;

	using Castle.Applications.MindDump.Dao;
	using Castle.Applications.MindDump.Model;
	using Castle.Applications.MindDump.Services;
	using Castle.MonoRail.Framework;


	public class BlogControllerCreatorSubscriber : IMindDumpEventSubscriber, IInitializable
	{
		private BlogDao _blogDao;
		private IControllerTree _controllerTree;

		public BlogControllerCreatorSubscriber(BlogDao blogDao, IControllerTree controllerTree)
		{
			_blogDao = blogDao;
			_controllerTree = controllerTree;
		}

		public void Initialize()
		{
			IList blogs = _blogDao.Find();

			foreach(Blog blog in blogs)
			{
				OnBlogAdded(blog);
			}
		}

		public void OnBlogAdded(Blog blog)
		{
			_controllerTree.AddController(String.Empty, blog.Author.Login, typeof(BlogController));
		}

		public void OnBlogRemoved(Blog blog)
		{
		}
	}
}
