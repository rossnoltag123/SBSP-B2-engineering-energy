﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorController : MonoBehaviour {

	public EnergyStorage energyStorage;

	private ReactorModel rm;
	public ReactorView rv;

	void Awake(){

		//Talk to Matt about replacing these 2 lines(ReactorView is not MonoBehaviour)
		Text energy = GameObject.Find ("energyLevel").GetComponent<Text>();
		Text storage = GameObject.Find ("energyStorageLevel").GetComponent<Text>();

		rm = new ReactorModel ();
		rv = new ReactorView (energy, storage);
	}

	void Start () {
		rm.SetFuel (10);
		rm.SetEnergy (0);

		energyStorage = new EnergyStorage ();

		InvokeRepeating("Producing", 1.0f, 1.0f);
	}
		


	public void Producing(){ 
		rm.SetEnergy(rm.GetEnergy() + rm.GetFuel());

		ShowEnergyLevel ();
		CheckingStorage ();
	}

	public void ShowEnergyLevel(){

		rv.SetEnergyLevel(rm.GetEnergy());
	}

	public void CheckingStorage(){

		if (rm.GetEnergy() >= rm.GetMaxcapacity()) {
			energyStorage.GetOkToDistribute ();

			energyStorage.SetCurrentCapacity (energyStorage.GetCurrentCapacity() +rm.GetMaxcapacity());
			rv.SetStorageLevel(energyStorage.GetCurrentCapacity()) ;

			rm.SetEnergy(rm.GetEnergy() - rm.GetMaxcapacity());
		}

		if (energyStorage.GetCurrentCapacity() >=  rm.GetMaxcapacity()) {
			energyStorage.SetOkToDistribute(true) ;
		}
	}

}