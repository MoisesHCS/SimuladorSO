// SIMULADOR SIMPLIFICADO DE ESCALONAMENTO DE CPU (FCFS)

using System;
using System.Collections.Generic;
using System.Linq;

// PROGRAMA PRINCIPAL
// id, chegada, duracao da CPU
var processos = new List<Processo>
{
    new Processo("P1", chegada: 0, duracaoCpu: 8),
    new Processo("P2", chegada: 1, duracaoCpu: 4),
    new Processo("P3", chegada: 2, duracaoCpu: 6),
    new Processo("P4", chegada: 3, duracaoCpu: 2),
};

var simulador = new Simulador();
simulador.Executar(processos);

// 1) O PROCESSO
class Processo
{
    public string Id;
    public long Chegada;      // instante em que o processo chega
    public long DuracaoCpu;   // quanto tempo de CPU ele precisa (surto unico)

    public long InstanteInicio = -1;  // quando comecou a executar (1a vez)
    public long InstanteFim = -1;     // quando terminou

    public Processo(string id, long chegada, long duracaoCpu)
    {
        Id = id;
        Chegada = chegada;
        DuracaoCpu = duracaoCpu;
    }

    public long TempoDeRetorno => InstanteFim - Chegada;
    public long TempoDeEspera => InstanteInicio - Chegada;
}

// 2) EVENTO
class Evento
{
    public long Tempo;
    public long Sequencia;   // desempate deterministico quando o tempo empata
    public string Tipo;      // "Chegada" ou "FimExecucao"
    public string ProcessoId;
}

// 3) SIMULADOR
class Simulador
{
    private long _clock = 0;
    private long _proximaSequencia = 0;
    private readonly List<Evento> _filaDeEventos = new();
    private readonly Queue<Processo> _filaDeProntos = new();
    private readonly Dictionary<string, Processo> _processos = new();
    private Processo? _emExecucao = null;

    public void Agendar(long tempo, string tipo, string processoId)
    {
        _filaDeEventos.Add(new Evento
        {
            Tempo = tempo,
            Sequencia = _proximaSequencia++,
            Tipo = tipo,
            ProcessoId = processoId
        });
    }

    private Evento RemoverProximoEvento()
    {
        // Pega o evento com menor tempo; em caso de empate, o que foi
        // agendado primeiro (garante resultado sempre igual pra mesma entrada).
        var proximo = _filaDeEventos
            .OrderBy(e => e.Tempo)
            .ThenBy(e => e.Sequencia)
            .First();

        _filaDeEventos.Remove(proximo);
        return proximo;
    }

    public void Executar(List<Processo> processos)
    {
        foreach (var p in processos)
        {
            _processos[p.Id] = p;
            Agendar(p.Chegada, "Chegada", p.Id);
        }

        Console.WriteLine("=== LOG DA SIMULACAO ===");

        while (_filaDeEventos.Count > 0)
        {
            var evento = RemoverProximoEvento();
            _clock = evento.Tempo;

            switch (evento.Tipo)
            {
                case "Chegada":
                    TratarChegada(evento.ProcessoId);
                    break;
                case "FimExecucao":
                    TratarFimDeExecucao(evento.ProcessoId);
                    break;
            }
        }

        ImprimirMetricas();
    }

    private void TratarChegada(string processoId)
    {
        var p = _processos[processoId];
        Console.WriteLine($"[t={_clock}] Chegada do processo {p.Id}");
        _filaDeProntos.Enqueue(p);
        TentarDespacharSeOcioso();
    }

    private void TratarFimDeExecucao(string processoId)
    {
        var p = _processos[processoId];
        p.InstanteFim = _clock;
        Console.WriteLine($"[t={_clock}] Processo {p.Id} finalizado");
        _emExecucao = null;
        TentarDespacharSeOcioso();
    }

    private void TentarDespacharSeOcioso()
    {
        if (_emExecucao is not null || _filaDeProntos.Count == 0)
            return;

        var proximo = _filaDeProntos.Dequeue();
        _emExecucao = proximo;
        proximo.InstanteInicio = _clock;

        Console.WriteLine($"[t={_clock}] Despacho do processo {proximo.Id} " +
                           $"(vai executar por {proximo.DuracaoCpu} unidades)");

        Agendar(_clock + proximo.DuracaoCpu, "FimExecucao", proximo.Id);
    }

    private void ImprimirMetricas()
    {
        Console.WriteLine();
        Console.WriteLine("=== METRICAS ===");
        Console.WriteLine($"{"Processo",-10}{"Chegada",-10}{"Inicio",-10}{"Fim",-10}{"Retorno",-10}{"Espera",-10}");

        foreach (var p in _processos.Values.OrderBy(p => p.Chegada))
        {
            Console.WriteLine($"{p.Id,-10}{p.Chegada,-10}{p.InstanteInicio,-10}{p.InstanteFim,-10}{p.TempoDeRetorno,-10}{p.TempoDeEspera,-10}");
        }

        double retornoMedio = _processos.Values.Average(p => p.TempoDeRetorno);
        double esperaMedia = _processos.Values.Average(p => p.TempoDeEspera);
        Console.WriteLine();
        Console.WriteLine($"Tempo de retorno medio: {retornoMedio:F2}");
        Console.WriteLine($"Tempo de espera medio:  {esperaMedia:F2}");
    }
}

