﻿using StayEase.Model.Enum;

namespace StayEase.Model.Entity.BaseResponce
{
	public class BaseResponse<T> : IBaseResponse<T>
	{
		public string Description { get; set; }

		public StatusCode StatusCode { get; set; }

		public T Data { get; set; }
	}

	public interface IBaseResponse<T>
	{
		StatusCode StatusCode { get; }
		T Data { get; }
		string Description { get; set; }
	}
}
