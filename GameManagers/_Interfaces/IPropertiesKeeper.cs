using System;
using System.Collections;
using System.Collections.Generic;



public interface IPropertiesKeeper<T>{
	T Load();
	void Save(T data);
}