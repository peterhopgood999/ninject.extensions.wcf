﻿//-------------------------------------------------------------------------------
// <copyright file="Service1.cs" company="Ninject Project Contributors">
//   Copyright (c) 2009-2011 Ninject Project Contributors
//   Author: Chris Hafey (chafey@gmail.com)
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   you may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace WcfRestService
{
	using System.Collections.Generic;
	using System.ServiceModel;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Web;

	// Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	// The purpose of the Ninject WCF Extension is to handle InstanceContextMode.PerCall service behavior.  The WCF REST approach does not have any support
	// for InstanceContext.PerSession so you should not be using it (if you need such functionality, implement your REST functionality using ASP.NET).  
	// While WCF REST does support InstanceContextMode.Single, you probably want to avoid using the Ninject WCF Extension with it since
	// it was designed to handle InstanceContextMode.PerCall and could cause some problems.  You also probalby want to avoid sharing the instance of the Ninject
	// Kernel used by the Ninject WCF Extension too.  For InstanceContextMode.Single services, you can use the core Ninject library.
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	public class Service1
	{
		private readonly IRepository _repository;

		// Implementing your WCF REST service using Interfaces (as opposed to concrete types) is the whole reason
		// for using the Ninject WCF Extension.  If you don't use the WCF Extension, you have to handle creation of the kernel
		// and per WCF REST Request deactivation yourself.  
		public Service1(IRepository repository)
		{
			_repository = repository;
		}

		[WebGet(UriTemplate = "")]
		public List<SampleItem> GetCollection()
		{
			// We moved the template generated functionality for GetCollection() to the Repository class which
			// implements the IRepository interface
			return _repository.GetCollection();
		}

		// The other methods automatically generated by the project template were deleted since only one
		// method is required to illustrate how to use the Ninject WCF Extension with WCF REST

	}
}