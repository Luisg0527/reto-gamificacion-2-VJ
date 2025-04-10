using System;
using UnityEngine;

public class Candidate
{
    public int id_candidato {get;set;}
	public string nombre {get;set;}
	public string domicilio {get;set;}
	public DateTime fecha_nacimiento {get;set;}
	public string nombre_uni {get;set;}
	public string carrera {get;set;}
	public string llamada_recomendacion {get;set;}
	public string trabajos {get;set;}
	public bool contratar {get;set;}

    public Candidate() {

    }

    public Candidate(int id_candidato_,string nombre_,string domicilio_,DateTime fecha_nacimiento_,string nombre_uni_,string carrera_,string llamada_recomendacion_,string trabajos_,bool contratar_)
	{
		this.id_candidato = id_candidato_;
		this.nombre = nombre_;
		this.domicilio = domicilio_;
		this.fecha_nacimiento = fecha_nacimiento_;
		this.nombre_uni = nombre_uni_;
		this.carrera = carrera_;
		this.llamada_recomendacion = llamada_recomendacion_;
		this.trabajos = trabajos_;
		this.contratar = contratar_;
	}
}
