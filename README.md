# Json.Flat
Provides static methods to flatten a Json object in to name/value pairs and back to the original Json structure.

## Example

```var flattened = JsonFlattener.Flatten(data);```

### Input
```
{
  "firstName" : "Test",
  "lastName" : "One",
  "address" : {
    "street" : "123 Street",
    "city" : "City", 
    "state" : "State",
    "phone" : [
      {
        "mobile" : "123-456-7890",
        "home" : "111-555-1219",
        "work" : "222-111-3389"
      },
      {
        "mobile" : "123-456-7890",
        "home" : "111-555-1219",
        "work" : "222-111-3389"
      }
    ]
  }
}
```

### Output
```
firstName, Test
lastName, One
address.street, 123 Street
address.city, City
address.state, State
address.phone[0].mobile, 123-456-7890
address.phone[0].home, 111-555-1219
address.phone[0].work, 222-111-3389
address.phone[1].mobile, 123-456-7890
address.phone[1].home, 111-555-1219
address.phone[1].work, 222-111-3389
```

```var unflattened = JsonFlattener.Unflatten(flattened);```

### Input
```
firstName, Test
lastName, One
address.street, 123 Street
address.city, City
address.state, State
address.phone[0].mobile, 123-456-7890
address.phone[0].home, 111-555-1219
address.phone[0].work, 222-111-3389
address.phone[1].mobile, 123-456-7890
address.phone[1].home, 111-555-1219
address.phone[1].work, 222-111-3389
```

### Output
```
{
  "firstName" : "Test",
  "lastName" : "One",
  "address" : {
    "street" : "123 Street",
    "city" : "City", 
    "state" : "State",
    "phone" : [
      {
        "mobile" : "123-456-7890",
        "home" : "111-555-1219",
        "work" : "222-111-3389"
      },
      {
        "mobile" : "123-456-7890",
        "home" : "111-555-1219",
        "work" : "222-111-3389"
      }
    ]
  }
}
```
