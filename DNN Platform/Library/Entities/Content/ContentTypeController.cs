﻿#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Entities.Content.Common;
using DotNetNuke.Entities.Content.Data;
using DotNetNuke.Entities.Users;

#endregion

namespace DotNetNuke.Entities.Content
{
	/// <summary>
	/// ContentTypeController provides the business layer of ContentType.
	/// </summary>
	/// <remarks>
	/// </remarks>
	/// <example>
	/// <code lang="C#">
	/// IContentTypeController typeController = new ContentTypeController();
    /// ContentType contentType = (from t in typeController.GetContentTypes()
    ///                            where t.ContentType == "DesktopModule"
    ///                            select t).SingleOrDefault();
    /// if(contentType == null)
    /// {
    ///     contentType = new ContentType {ContentType = "DesktopModule"};
    ///     contentType.ContentTypeId = typeController.AddContentType(contentType);
    /// }
	/// </code>
	/// </example>
    public class ContentTypeController : IContentTypeController
    {
        private readonly IDataContext _dataContext; 
        
        public ContentTypeController() : this(DataContext.Instance()) { }

        public ContentTypeController(IDataContext dataContext)
        {
            //Argument Contract
            Requires.NotNull("dataContext", dataContext);

            _dataContext = dataContext;
        }

		/// <summary>
		/// Adds the type of the content.
		/// </summary>
		/// <param name="contentType">Type of the content.</param>
		/// <returns>content type id.</returns>
		/// <exception cref="System.ArgumentNullException">content type is null.</exception>
		/// <exception cref="System.ArgumentException">contentType.ContentType is empty.</exception>
        public int AddContentType(ContentType contentType)
        {
            //Argument Contract
            Requires.NotNull("contentType", contentType);
            Requires.PropertyNotNullOrEmpty("contentType", "ContentType", contentType.ContentType);

            using (_dataContext)
            {
                var rep = _dataContext.GetRepository<ContentType>();

                rep.Insert(contentType);
            }

            return contentType.ContentTypeId;
        }

		/// <summary>
		/// Clears the content type cache.
		/// </summary>
        public void ClearContentTypeCache()
        {
            DataCache.RemoveCache(DataCache.ContentTypesCacheKey);
        }

		/// <summary>
		/// Deletes the type of the content.
		/// </summary>
		/// <param name="contentType">Type of the content.</param>
		/// <exception cref="System.ArgumentNullException">content type is null.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">content type id is less than 0.</exception>
        public void DeleteContentType(ContentType contentType)
        {
            //Argument Contract
            Requires.NotNull("contentType", contentType);
            Requires.PropertyNotNegative("contentType", "ContentTypeId", contentType.ContentTypeId);

            using (_dataContext)
            {
                var rep = _dataContext.GetRepository<ContentType>();

                rep.Delete(contentType);
            }
        }

		/// <summary>
		/// Gets the content types.
		/// </summary>
		/// <returns>content type collection.</returns>
        public IQueryable<ContentType> GetContentTypes()
		{
		    IQueryable<ContentType> contentTypes;
            using (_dataContext)
		    {
                var rep = _dataContext.GetRepository<ContentType>();

		        contentTypes = rep.Get().AsQueryable();
		    }

		    return contentTypes;
        }

        /// <summary>
        /// Gets the content types for a specific portal.
        /// </summary>
        /// <param name="portalId">The portalId</param>
        /// <returns>content type collection.</returns>
        public IQueryable<ContentType> GetContentTypes(int portalId)
	    {
            IQueryable<ContentType> contentTypes;
            using (_dataContext)
            {
                var rep = _dataContext.GetRepository<ContentType>();

                contentTypes = rep.Get(portalId).AsQueryable();
            }

            return contentTypes;
        }

	    /// <summary>
		/// Updates the type of the content.
		/// </summary>
		/// <param name="contentType">Type of the content.</param>
		/// <exception cref="System.ArgumentNullException">content type is null.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">content type id is less than 0.</exception>
		/// <exception cref="System.ArgumentException">contentType.ContentType is empty.</exception>
        public void UpdateContentType(ContentType contentType)
        {
            //Argument Contract
            Requires.NotNull("contentType", contentType);
            Requires.PropertyNotNegative("contentType", "ContentTypeId", contentType.ContentTypeId);
            Requires.PropertyNotNullOrEmpty("contentType", "ContentType", contentType.ContentType);

		    using (_dataContext)
		    {
		        var rep = _dataContext.GetRepository<ContentType>();

                rep.Update(contentType);
		    }
        }

        [Obsolete("Deprecated in DNN 8.0.0")]
        public ContentTypeController(IDataService dataService) { }
    }
}