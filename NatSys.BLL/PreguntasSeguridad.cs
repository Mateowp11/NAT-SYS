// Proyecto: NatSys.BLL / Seguridad
// Preguntas fijas para elegir al dar de alta un usuario. Preguntas predefinidas
// (en vez de texto libre) evitan respuestas demasiado obvias o mal pensadas.

namespace NatSys.BLL
{
    public static class PreguntasSeguridad
    {
        public static readonly string[] Disponibles = new[]
        {
            "¿Cuál es tu comida favorita?",
            "¿Cuál es tu nadador favorito?",
            "¿Cuál es tu prueba favorita?",
            "¿Cuál es tu estilo favorito?"
        };
    }
}