﻿namespace Kariyer.Model.Entities;

public abstract class BaseEntity {

    public int Id { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	public DateTime? UpdatedDate { get; set; }
}