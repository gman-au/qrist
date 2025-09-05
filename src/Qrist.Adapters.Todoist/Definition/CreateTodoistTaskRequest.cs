using System.Text.Json.Serialization;

namespace Qrist.Adapters.Todoist.Definition
{
    public class CreateTodoistTaskRequest
    {
        [JsonPropertyName("content")] public string Content { get; set; }

        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("project_id")] public string ProjectId { get; set; }

        [JsonPropertyName("section_id")] public string SectionId { get; set; }

        [JsonPropertyName("parent_id")] public string ParentId { get; set; }

        [JsonPropertyName("order")] public int? Order { get; set; }

        [JsonPropertyName("labels")] public string[] Labels { get; set; }

        [JsonPropertyName("priority")] public int Priority { get; set; }

        [JsonPropertyName("assignee_id")] public int? AssigneeId { get; set; }

        [JsonPropertyName("due_string")] public string DueString { get; set; }

        [JsonPropertyName("due_date")] public string DueDate { get; set; }

        [JsonPropertyName("due_datetime")] public string DueDateTime { get; set; }

        [JsonPropertyName("due_lang")] public string DueLang { get; set; }

        [JsonPropertyName("duration")] public int? Duration { get; set; }

        [JsonPropertyName("duration_unit")] public string DurationUnit { get; set; }

        [JsonPropertyName("deadline_date")] public string DeadlineDate { get; set; }

        [JsonPropertyName("deadline_lang")] public string DeadlineLang { get; set; }
    }
}