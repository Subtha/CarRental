﻿namespace CarRental.Application.Common
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
