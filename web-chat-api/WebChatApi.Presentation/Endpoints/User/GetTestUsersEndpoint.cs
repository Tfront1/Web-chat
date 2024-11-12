using FastEndpoints;
using WebChatApi.Domain.Dbos;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.User;

public class GetTestUsersEndpoint : Endpoint<EmptyRequest, List<UserDbo>>
{
	public GetTestUsersEndpoint()
	{
	}

	public override void Configure()
	{
		AllowAnonymous();
		Get("test/get");
		Group<UserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get");
		});
		Summary(s =>
		{
			s.Summary = "Get test";
			s.Description = "Get test";
		});
	}

	public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
	{
		var accounts = new List<UserDbo>
		{
			new UserDbo
			{
				Id = 123,
				Username = "tarasdev",
				AvatarUrl = "/images/avatars/tarasdev.jpg",
				SubscriptionAmount = 0,
				FirstName = "Тарас",
				LastName = "Шевченко",
				IsActive = true,
				Stack = new List<string>
				{
					"Python",
					"Django",
					"FastAPI",
					"React",
					"SQLAlchemy",
					"PostgreSQL",
					"Alembic",
					"Docker"
				},
				City = "Новояворівськ",
				Description = "Full-stack розробник з 5-річним досвідом. Спеціалізуюсь на Python та React. Люблю створювати масштабовані веб-додатки та вивчати нові технології."
			},
			new UserDbo
			{
				Id = 124,
				Username = "olenkadev",
				AvatarUrl = "/images/avatars/olenkadev.jpg",
				SubscriptionAmount = 99.99m,
				FirstName = "Оленка",
				LastName = "Петренко",
				IsActive = true,
				Stack = new List<string>
				{
					"JavaScript",
					"React",
					"Node.js",
					"MongoDB",
					"Express"
				},
				City = "Київ",
				Description = "Frontend розробниця, яка захоплюється створенням красивих та інтуїтивних інтерфейсів. Активно вивчаю Node.js та працюю над власними проектами."
			},
			new UserDbo
			{
				Id = 125,
				Username = "ostapdev",
				AvatarUrl = "/images/avatars/ostapdev.jpg",
				SubscriptionAmount = 149.99m,
				FirstName = "Остап",
				LastName = "Коваленко",
				IsActive = true,
				Stack = new List<string>
				{
					"C#",
					".NET",
					"Angular",
					"SQL Server",
					"Azure"
				},
				City = "Львів",
				Description = "Досвідчений .NET розробник з глибокими знаннями Azure. Захоплююсь архітектурою програмного забезпечення та паттернами проектування."
			},
			new UserDbo
			{
				Id = 126,
				Username = "sofiaweb",
				AvatarUrl = "/images/avatars/sofiaweb.jpg",
				SubscriptionAmount = 0,
				FirstName = "Софія",
				LastName = "Іваненко",
				IsActive = false,
				Stack = new List<string>
				{
					"HTML",
					"CSS",
					"JavaScript",
					"Vue.js",
					"Firebase"
				},
				City = "Одеса",
				Description = "Починаюча веб-розробниця з художньою освітою. Поєдную дизайнерське мислення з технічними навичками для створення унікальних веб-проектів."
			},
			new UserDbo
			{
				Id = 127,
				Username = "bogdanops",
				AvatarUrl = "/images/avatars/bogdanops.jpg",
				SubscriptionAmount = 199.99m,
				FirstName = "Богдан",
				LastName = "Мельник",
				IsActive = true,
				Stack = new List<string>
				{
					"Kubernetes",
					"Docker",
					"AWS",
					"Jenkins",
					"Terraform",
					"Python"
				},
				City = "Харків",
				Description = "DevOps інженер з великим досвідом в автоматизації та побудові CI/CD pipeline. Захоплююсь контейнеризацією та мікросервісною архітектурою."
			}
		};

		Response = accounts;
	}
}