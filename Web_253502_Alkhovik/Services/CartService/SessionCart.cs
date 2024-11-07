using System.Text.Json.Serialization;
using Web_253502_Alkhovik.Domain.Entities;
using Web_253502_Alkhovik.Extensions;

namespace Web_253502_Alkhovik.Services.CartService;

public class SessionCart : Cart
{
	[JsonIgnore]
	public ISession? Session { get; set; }
	public static Cart GetCart(IServiceProvider services)
	{
		ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
		SessionCart cart = session.Get<SessionCart>("cart") ?? new SessionCart();
		cart.Session = session;
		return cart;
	}
	public override void AddToCart(Car car)
	{
		base.AddToCart(car);
		Session?.Set("cart", this);
	}
	public override void RemoveItems(int id)
	{
		base.RemoveItems(id);
		Session?.Set("cart", this);
	}
	public override void ClearAll()
	{
		base.ClearAll();
		Session?.Remove("cart");
	}
}