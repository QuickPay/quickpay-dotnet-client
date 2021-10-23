﻿using System;
using System.Collections.Generic;
using RestSharp;
using Quickpay.Util;
using Quickpay.Models;
using Quickpay.Models.Account;

namespace Quickpay
{
	// Please note:
	// This is only an example implementation, do not use this for production; this class is not updated as new endpoints are created!!!
	// models are created by inserting a json to this page http://json2csharp.com/#

	public class MerchantExample : QuickPayRestClient
	{
		public MerchantExample (string username, string password) : base (username, password)
		{
		}

		public MerchantExample (string apikey) : base (apikey)
		{
		}

		public Account Account ()
		{
			return CallEndpoint<Account> ("account");
		}

		public List<AclResource> AclResources (AccountType accountType = AccountType.Any, PageParameters? pageParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				if (accountType != AccountType.Any)
					request.AddParameter ("account_type", accountType.GetName ());
			}; 

			return CallEndpoint<List<AclResource>> ("acl-resources", prepareRequest);
		}

		public List<Agreement> Agreements (PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				AddSortingParameters (sortingParameters, request);
			}; 
			return CallEndpoint<List<Agreement>> ("agreements", prepareRequest);
		}

		public List<Payment> Payments (PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				AddSortingParameters (sortingParameters, request);
			};
			return CallEndpoint<List<Payment>> ("payments", prepareRequest);
		}

		public List<Branding> Branding (PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				AddSortingParameters (sortingParameters, request);
			};
			return CallEndpoint<List<Branding>> ("brandings", prepareRequest);
		}

		public List<Activity> Activity (PageParameters? pageParameters = null, SortingParameters? sortingParameters = null)
		{
			Action<RestRequest> prepareRequest = (RestRequest request) => {
				AddPagingParameters (pageParameters, request);
				AddSortingParameters (sortingParameters, request);
			};
			return CallEndpoint<List<Activity>> ("activity", prepareRequest);
		}

		public List<AcquirerStatus> AcquirerOperationalStatus ()
		{
			return CallEndpoint<List<AcquirerStatus>> ("operational-status/acquirers");
		}
	}
}

