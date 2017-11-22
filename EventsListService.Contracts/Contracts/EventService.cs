using EventsListService.Contracts.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsListService.Contracts.Contracts
{
    public class EventService : IEventService
    {

        public List<EventDto> Events = new List<EventDto>
        {
            new EventDto
            {
                Id = 1,
                Name = "Творческая встреча 1",
                OrganizerId = 1,
                SubcategoryId = 1,
                Date = new DateTime(2017,11,15),
                Address = "Кафе \"Огонёк\"",
                Description = "Творческая встреча творческих людей",
                ImageUrl = ""
            },
            new EventDto
            {
                Id = 2,
                Name = "Творческая встреча 2",
                OrganizerId = 1,
                SubcategoryId = 1,
                Date = new DateTime(2017,11,25),
                Address = "Кафе \"Огонёк\"",
                Description = "Творческая встреча творческих людей",
                ImageUrl = ""
            },
            new EventDto
            {
                Id = 3,
                Name = "Что?Где?Когда?",
                OrganizerId = 1,
                SubcategoryId = 8,
                Date = new DateTime(2017,11,20),
                Address = "Беларусь, Могилёвская область, Могилёв, Пушкинский просп., 7",
                Description = "Интелектуальная игра \"Что?Где?Когда?\"",
                ImageUrl = ""
            },
            new EventDto
            {
                Id = 4,
                Name = "Что?Где?Когда?",
                OrganizerId = 1,
                SubcategoryId = 8,
                Date = new DateTime(2017,11,02),
                Address = "Беларусь, Могилёвская область, Могилёв, Пушкинский просп., 7",
                Description = "Интелектуальная игра \"Что?Где?Когда?\"",
                ImageUrl = ""
            },
            new EventDto
            {
                Id = 5,
                Name = "Конференция по блокчейну",
                OrganizerId = 1,
                SubcategoryId = 5,
                Date = new DateTime(2017,11,28),
                Address = "Беларусь, Минск, просп. Победителей, 7А",
                Description = "Конференция",
                ImageUrl = ""
            }
        };

        public List<OrganizerDto> Organizers = new List<OrganizerDto>
        {
            new OrganizerDto
            {
                Id = 1,
                Name = "ШоуМастер",
                Emails = new List<string> {@"show.master@yandex.by"},
                Phones = new List<string> {"+375-29-111-11-11", "+375-29-123-12-21"}
            },
            new OrganizerDto
            {
                Id = 2,
                Name = "Conference",
                Emails = new List<string> {@"conference.minsk@yandex.by", @"conference.mogilev@yandex.by"},
                Phones = new List<string> {"+375-29-909-01-14"}
            }
        };

        public List<CategoryDto> Categories = new List<CategoryDto>
        {
            new CategoryDto {Id = 1, Name = "Творческие",Subcategories =
            {
                new SubcategoryDto {Id = 1, CategoryId = 1, Name = "Творческая встреча"},
                new SubcategoryDto {Id = 2, CategoryId = 1, Name = "Творческий вечер"},
                new SubcategoryDto {Id = 3, CategoryId = 1, Name = "Творческий проект"}
            }},
            new CategoryDto {Id = 2, Name = "Досуг",Subcategories =
            {
                new SubcategoryDto {Id = 7,CategoryId = 2, Name = "Досуговая программа"},
                new SubcategoryDto {Id = 8,CategoryId = 2, Name = "Интеллектуальная игра"},
            }},
            new CategoryDto {Id = 3, Name = "Конференция", Subcategories =
            {
                new SubcategoryDto {Id = 4, CategoryId = 3, Name = "Бизнесс конференция"},
                new SubcategoryDto {Id = 5, CategoryId = 3, Name = "IT конференция"},
                new SubcategoryDto {Id = 6, CategoryId = 3, Name = "Форум"}
            }},
            new CategoryDto {Id = 4, Name = "Музыкальные",Subcategories =
            {
                new SubcategoryDto {Id = 10,CategoryId = 4, Name = "Фестиваль"},
                new SubcategoryDto {Id = 11,CategoryId = 4, Name = "Концерт"}
            }},
            new CategoryDto {Id = 5, Name = "На открытом воздухе",Subcategories =
            {
                new SubcategoryDto {Id = 10,CategoryId = 5, Name = "Велособытие"},
                new SubcategoryDto {Id = 11,CategoryId = 5, Name = "Салют"}
            }}

        };


        //new Category {Id = 1, Name = "Автограф-сессия"},
        //new Category {Id = 2, Name = "Благотворительность"},
        //new Category {Id = 3, Name = ""},
        //new Category {Id = 7, Name = "Конкурс"},
        //new Category {Id = 9, Name = "Модный маркет"},
        //new Category {Id = 10, Name = "Награждение"},
        //new Category {Id = 11, Name = "Новогоднее представление"},
        //new Category {Id = 12, Name = "Презентация"},
        //new Category {Id = 16, Name = "Фестиваль"},
        //new Category {Id = 18, Name = "Экскурсия"},
        //new Category {Id = 19, Name = "Ярмарка"}

        public List<EventDto> GetEvents()
        {
            return Events;
        }

        public List<CategoryDto> GetCategories()
        {
            return Categories;
        }

        public List<SubcategoryDto> GetSubcategories()
        {
            return Categories.SelectMany(x => x.Subcategories.Select(z => z)).ToList();
        }

        public List<OrganizerDto> GetOrganizers()
        {
            return Organizers;
        }
    }
}
